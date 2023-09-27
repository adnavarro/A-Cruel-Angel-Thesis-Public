using System;
using TMPro;
using UnityEngine;

public class SaveSign : MonoBehaviour
{
    private bool _isPlayerInRange;
    private SaveDialogueManager _saveDialogueManager;
    public TMP_FontAsset fontAsset;
    public Dialogue dialogue;

    private void Start()
    {
        _isPlayerInRange = false;
        try
        {
            _saveDialogueManager = FindObjectOfType<SaveDialogueManager>();
            
        }
        catch (Exception e)
        {
            Debug.Log(e + " The NPC GameObject called " + name + " is not in the scene");
        }
    }
    
    private void Update()
    {
        OnInteract();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
    
    private void OnInteract()
    {
        if (GameManager.Instance.OnInteract("npc") && _isPlayerInRange)
        {
            if (!_saveDialogueManager.IsConversationActive)
            {
                StartCoroutine(_saveDialogueManager.InitializeDialogue(dialogue, fontAsset));
            }
        }
    }
}
