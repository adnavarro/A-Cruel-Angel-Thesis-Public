using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public enum QuestType
{
    TalkToNpc,
    GiveItemToNpc,
    None
}

public class Npc : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private new string name;
    [SerializeField] private Sprite npcFace;
    [SerializeField] private Enemy enemy;
    [SerializeField] private bool isQuestNpc;
    [SerializeField] private QuestType questType;
    [SerializeField] private bool isOptionalAndAppearedBefore;
    [SerializeField] private bool noInteractuable;
    private bool _isPlayerInRange;
    private NpcDialogueManager _npcDialogueManager;
    private int _dialogueId;
    public List<DialogueOption> dialogueOptions;
    public DialogueOption rareDialogue;
    public TMP_FontAsset fontAsset;
    private bool _isQuestGiven;

    #region Properties
    
    public int Id => id;
    public Enemy Enemy => enemy;
    public string Name => name;
    public bool IsQuestNpc => isQuestNpc;
    public Sprite NpcFace => npcFace;
    public QuestType QuestType => questType;
    public int DialogueId => _dialogueId;
    public bool IsQuestGiven
    {
        get => _isQuestGiven;
        set => _isQuestGiven = value;
    }

    public bool NoInteractuable
    {
        get => noInteractuable;
        set => noInteractuable = value;
    }

    #endregion

    private void OnEnable()
    {
        LoadNpcState();
        EventManager.OnPortalStepped += ActivateRareDialogue;
    }

    private void OnDestroy()
    {
        SaveNpcState();
        EventManager.OnPortalStepped -= ActivateRareDialogue;
    }

    private void Start()
    {
        InitializeNpc();
    }

    private void Update()
    {
        OnInteract();
    }

    private void InitializeNpc()
    {
        _isPlayerInRange = false;
        _isQuestGiven = false;

        if (!IsQuestNpc)
        {
            questType = QuestType.None;
        }

        try
        {
            _npcDialogueManager = FindObjectOfType<NpcDialogueManager>();
        }
        catch (Exception e)
        {
            Debug.Log(e + " The NPC GameObject called " + name + " is not in the scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!noInteractuable)
        {
            if (other.CompareTag("Player"))
            {
                EnemyGenerator.IsGeneratorPaused = true;
                _isPlayerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!noInteractuable)
        {
            if (other.CompareTag("Player"))
            {
                EnemyGenerator.IsGeneratorPaused = false;
                _isPlayerInRange = false;
            }
        }
    }

    private void OnInteract()
    {
        if (!noInteractuable)
        {
            if (GameManager.Instance.OnInteract("npc") && _isPlayerInRange)
            {
                if (!_npcDialogueManager.IsConversationActive)
                {
                    StartDialogue();
                }
            }
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(_npcDialogueManager.InitializeDialogue(this));
    }

    public void UpdateDialogue(int nextId)
    {
        if (_dialogueId < dialogueOptions.Count - 1)
        {
            if (dialogueOptions[_dialogueId].NextDialogueOptionIds.Count == 1)
            {
                var condition = dialogueOptions[nextId].Condition;
                if (!condition)
                {
                    _dialogueId = nextId;
                }
                else if (condition.EvaluateCondition())
                {
                    _dialogueId = nextId;
                }
            }
            else if(dialogueOptions[_dialogueId].NextDialogueOptionIds.Count > 1)
            {
                StopCoroutine(_npcDialogueManager.ShowOptions());
                _dialogueId = nextId;
                _npcDialogueManager.StartDialogue(dialogueOptions[_dialogueId], fontAsset);
            }
            SaveNpcState();
        }
    }

    private void ActivateRareDialogue()
    {
        StartCoroutine(ShowRareDialogue());
    }

    private IEnumerator ShowRareDialogue()
    {
        _npcDialogueManager.Npc = this;
        _npcDialogueManager.StartDialogue(rareDialogue, fontAsset);
        
        while (!_npcDialogueManager.ConversationFinished)
        {
            if (GameManager.Instance.OnInteract("npc"))
            {
                _npcDialogueManager.DisplayNextSentence(fontAsset);
            }

            yield return null;
        }
    }

    private void SaveNpcState()
    {
        PlayerPrefs.SetInt(Id + "NpcDialogueIndex", _dialogueId);
    }

    private void LoadNpcState()
    {
        if (PlayerPrefs.HasKey(Id + "NpcDialogueIndex"))
        {
            _dialogueId = PlayerPrefs.GetInt(Id + "NpcDialogueIndex");
        }
    }
}
