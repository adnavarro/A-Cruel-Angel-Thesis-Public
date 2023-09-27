using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject saveText;
    [SerializeField] private Animator dialogueBoxAnimator;
    [SerializeField] private List<GameObject> mainButtons;
    private Queue<string> _sentences;

    public bool ConversationFinished { get; set; }
    public bool IsConversationActive { get; private set; }
    
    private void Awake()
    {
        _sentences = new Queue<string>();
    }

    private void Start()
    {
        ConversationFinished = true;
        IsConversationActive = false;
        saveText.SetActive(false);
        mainButtons[0].SetActive(false);
        mainButtons[1].SetActive(false);
    }
    
    public IEnumerator InitializeDialogue(Dialogue dialogue, TMP_FontAsset fontAsset)
    {
        IsConversationActive = true;
        while (IsConversationActive)
        {
            if (ConversationFinished)
            {
                StartDialogue(dialogue, fontAsset);
            }
            else
            {
                if (GameManager.Instance.OnInteract("npc"))
                {
                    DisplayNextSentence(fontAsset);
                }
            }
            yield return null;
        }
    }

    public void StartDialogue(Dialogue dialogue, TMP_FontAsset fontAsset)
    {
        GameManager.Instance.Player.PlayerMovement.DisableMovement();
        GameManager.Instance.IsOpenCloseInventoryActive = false;
        EnemyGenerator.IsGeneratorPaused = true;
        ConversationFinished = false;
        dialogueBoxAnimator.SetBool("IsOpen", true);
        
        _sentences.Clear();
        foreach (var sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence(fontAsset);
    }

    public void DisplayNextSentence(TMP_FontAsset fontAsset)
    {
    
        if (_sentences.Count == 0)
        {
            dialogText.text = "";
            saveText.SetActive(true);
            mainButtons[0].SetActive(true);
            mainButtons[1].SetActive(true);
            StartCoroutine(ShowSaveOptions());
            return;
        }
        var sentence = _sentences.Dequeue();
        dialogText.font = fontAsset;
        dialogText.text = sentence;
    }

    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("IsOpen", false);
        ConversationFinished = true;
        IsConversationActive = false;
        EnemyGenerator.IsGeneratorPaused = false;
        GameManager.Instance.IsOpenCloseInventoryActive = true;
        GameManager.Instance.Player.PlayerMovement.EnableMovement();
        
        saveText.SetActive(false);
        mainButtons[0].SetActive(false);
        mainButtons[1].SetActive(false);
    }

    private IEnumerator ShowSaveOptions()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(mainButtons[0]);
        while (true)
        {
            if (GameManager.Instance.OnInteract("ui"))
            {
                if (EventSystem.current.currentSelectedGameObject == mainButtons[0])
                {
                    //GameManager.Instance.SaveData();
                    break;
                }
                if (EventSystem.current.currentSelectedGameObject == mainButtons[1])
                {
                    break;
                }
            }
            yield return null;
        }
        EndDialogue();
    }
}
