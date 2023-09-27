using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatDialogueManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI dialogText;
	[SerializeField] private TMP_FontAsset combatFont;
	private CombatCanvas _combatCanvas;
	private Queue<string> _sentences;
	
	public bool DialogueIsActive { get; set; }

	private void Awake()
	{
		_sentences = new Queue<string>();
		InitializeManager();
	}

	private void InitializeManager()
	{
		DialogueIsActive = false;
	}
	
	public void StartDialogue(Dialogue dialogue, TMP_FontAsset fontAsset)
	{
		DialogueIsActive = true;
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
			EndDialogue();
			return;
		}

		if (fontAsset)
		{
			dialogText.font = fontAsset;
			dialogText.color = new Color32(255, 255, 255, 255);
		}
		else
		{
			dialogText.color = new Color32(51, 204, 255, 255);
			dialogText.font = combatFont;
		}
		dialogText.text = "";
		var sentence = _sentences.Dequeue();
		StartCoroutine(WriteSentence(sentence));
	}

	public void EndDialogue()
	{
		DialogueIsActive = false;
	}
	
	private IEnumerator WriteSentence(string sentence)
	{
		GameManager.Instance.IsInteractActive = false;

		foreach (var character in sentence.ToCharArray())
		{
			dialogText.text += character;
			yield return new WaitForSeconds(0.02f);
		}
		GameManager.Instance.IsInteractActive = true;
	}

	public void DisplaySentence(string sentence)
	{
		dialogText.text = sentence;
	}
}