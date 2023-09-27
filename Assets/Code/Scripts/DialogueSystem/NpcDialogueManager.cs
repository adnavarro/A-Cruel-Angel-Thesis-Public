using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NpcDialogueManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI dialogText;
	[SerializeField] private Animator dialogueBoxAnimator;
	[SerializeField] private Image npcFace;
	[SerializeField] private List<GameObject> optionButtons;
	[SerializeField] private List<TextMeshProUGUI> optionButtonsTexts;
	[FormerlySerializedAs("TextPanel")] [SerializeField] private TextMeshProUGUI textPanel;
	private DialogueOption _dialogueOption;
	private Queue<string> _sentences;
	public Npc Npc { get; set; }
	public bool ConversationFinished { get; private set; }
	public bool IsConversationActive { get; private set; }
	
	private void Awake()
	{
		_sentences = new Queue<string>();
	}

	private void Start()
	{
		ConversationFinished = true;
		IsConversationActive = false;
		optionButtons[0].SetActive(false);
		optionButtons[1].SetActive(false);
	}

	public IEnumerator InitializeDialogue(Npc npc)
	{
		IsConversationActive = true;
		Npc = npc;
		var dialogueOption = npc.dialogueOptions[npc.DialogueId];

		while (IsConversationActive)
		{
			if (ConversationFinished)
			{
				if (npc.IsQuestNpc && npc.QuestType == QuestType.GiveItemToNpc)
				{
					EventManager.OnNpcDialogue?.Invoke(npc);
				}
            
				StartDialogue(npc.dialogueOptions[npc.DialogueId], npc.fontAsset);

				if (npc.IsQuestNpc && npc.QuestType == QuestType.TalkToNpc)
				{
					EventManager.OnNpcDialogue?.Invoke(npc);
				}
			}
			else
			{
				if (GameManager.Instance.OnInteract("npc"))
				{
					DisplayNextSentence(npc.fontAsset);
				}
			}
			yield return null;
		}
	}

	public void StartDialogue(DialogueOption dialogueOption, TMP_FontAsset fontAsset)
	{
		_dialogueOption = dialogueOption;
		GameManager.Instance.Player.PlayerMovement.DisableMovement();
		GameManager.Instance.IsOpenCloseInventoryActive = false;
		EnemyGenerator.IsGeneratorPaused = true;
		ConversationFinished = false;
		npcFace.sprite = Npc.NpcFace;
		dialogueBoxAnimator.SetBool("IsOpen", true);
		
		nameText.text = Npc.Name != "" ? Npc.Name : "???";
		
		_sentences.Clear();
		foreach (var sentence in _dialogueOption.Dialogue.Sentences)
		{
			_sentences.Enqueue(sentence); 
		}
		DisplayNextSentence(fontAsset);
	}

	public void DisplayNextSentence(TMP_FontAsset fontAsset)
	{
		if (_sentences.Count == 0)
		{
			if (_dialogueOption.NextDialogueOptionIds.Count > 1)
			{
				dialogText.text = "";
				optionButtonsTexts[0].text = Npc.dialogueOptions[_dialogueOption.NextDialogueOptionIds[0]].ButtonText;
				optionButtonsTexts[1].text = Npc.dialogueOptions[_dialogueOption.NextDialogueOptionIds[1]].ButtonText;
				optionButtons[0].SetActive(true);
				optionButtons[1].SetActive(true);
				StartCoroutine(ShowOptions());
				return;
			}
			if (_dialogueOption.NextDialogueOptionIds.Count == 1)
			{
				Npc.UpdateDialogue(_dialogueOption.NextDialogueOptionIds[0]);
			}
			EndDialogue();
			return;
		}
		dialogText.font = fontAsset;
		dialogText.text = "";
		var sentence = _sentences.Dequeue();
		StopCoroutine(WriteSentence(sentence));
		StartCoroutine(WriteSentence(sentence));
	}

	private IEnumerator WriteSentence(string sentence)
	{
		GameManager.Instance.IsInteractActive = false;
		
		SetFontSize(sentence.ToCharArray());

		foreach (var character in sentence.ToCharArray())
		{
			dialogText.text += character;
			yield return new WaitForSeconds(0.02f);
		}
		GameManager.Instance.IsInteractActive = true;
	}

	private void SetFontSize(char[] sentence)
	{
		if (sentence.Length <= 17)
		{
			dialogText.fontSize = 8;
		}
		else if (sentence.Length > 17 && sentence.Length <= 48)
		{
			dialogText.fontSize = 6;
		}
		else if (sentence.Length > 48)
		{
			dialogText.fontSize = 4;
		}
	}

	public IEnumerator ShowOptions()
	{
		ConversationFinished = true;
		StopCoroutine(InitializeDialogue(Npc));
		
		yield return null;
		EventSystem.current.SetSelectedGameObject(optionButtons[0]);
		while (true)
		{
			if (GameManager.Instance.OnInteract("ui"))
			{
				if (EventSystem.current.currentSelectedGameObject == optionButtons[0])
				{
					if(_dialogueOption.NextDialogueOptionIds.Count > 0)
						Npc.UpdateDialogue(_dialogueOption.NextDialogueOptionIds[0]);
					break;
				}
				if (EventSystem.current.currentSelectedGameObject == optionButtons[1])
				{
					if(_dialogueOption.NextDialogueOptionIds.Count > 0)
						Npc.UpdateDialogue(_dialogueOption.NextDialogueOptionIds[1]);
					break;
				}
			}
			yield return null;
		}
		optionButtons[0].SetActive(false);
		optionButtons[1].SetActive(false);
	}

	private void EndDialogue()
	{
		ConversationFinished = true;
		dialogueBoxAnimator.SetBool("IsOpen", false);
		optionButtons[0].SetActive(false);
		optionButtons[1].SetActive(false);

		if (_dialogueOption.Quest || _dialogueOption.Item || _dialogueOption.SymptomAttached)
		{
			if (_dialogueOption.Quest && !Npc.IsQuestGiven)
			{
				GiveQuest();
			}

			if (_dialogueOption.Item)
			{
				GiveItem();
			}

			GameManager.Instance.SaveData();
		}
		
		if (_dialogueOption.Reward)
		{
			_dialogueOption.Reward.GiveReward();
		}
		
		if (_dialogueOption.EndsOnBattle)
        {
	        EnemyGenerator.IsGeneratorPaused = true;
	        GameManager.Instance.CurrentEnemy = Npc.Enemy;
			GameManager.Instance.StartBattleScene();
        }
		else
		{
			GameManager.Instance.IsOpenCloseInventoryActive = true;
			GameManager.Instance.Player.PlayerMovement.EnableMovement();
		}
		
		IsConversationActive = false;
		StopCoroutine(InitializeDialogue(Npc));
	}

	private void GiveQuest()
	{
		var player = GameManager.Instance.Player;
		if (!player.Backpack.Quests.Contains(_dialogueOption.Quest))
		{
			player.Backpack.AddQuest(_dialogueOption.Quest);
		}

		Npc.IsQuestGiven = true;
	}

	private void GiveItem()
	{
		_dialogueOption.Item.Quantity++;
		var player = GameManager.Instance.Player;
		if (!player.Backpack.Items.Contains(_dialogueOption.Item))
		{
			player.Backpack.AddItem(_dialogueOption.Item);
		}
	}
}