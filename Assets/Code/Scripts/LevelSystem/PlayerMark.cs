using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMark : MonoBehaviour
{
    private bool _isPlayerInRange;
    private bool _isActive;
    [SerializeField] private int _interactionCount; // Numero de veces que se interactuo con la marca
    [SerializeField] private int _interactionLimit; // Numero maximo de veces que se puede interactuar con la marca
    [SerializeField] private ItemConsumable _giveItem;
    [SerializeField] private float probabilityForEnemyToAppear = 30f;
    [SerializeField] private List<Enemy> enemiesToAppear;
    [SerializeField] private Condition itemToUseMark;
    
    public List<DialogueOption> dialogueOptions;
    public TMP_FontAsset fontAsset;
    [SerializeField] private Sprite npcFace;
    

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
        if(!FindObjectOfType<NpcDialogueManager>().IsConversationActive)
        if (GameManager.Instance.OnInteract("npc") && _isPlayerInRange && itemToUseMark.EvaluateCondition())
        {
            EnemyGenerator.IsGeneratorPaused = true;
            GameManager.Instance.DisablePlayerMovement();
            if (_isActive == false)
            {
                _isActive = true;
                StartCoroutine(Fishing());
                _interactionCount += 1;
            }
        }
    }

    private IEnumerator Fishing()
    {
        GameManager.Instance.IsInteractActive = false;
        GameManager.Instance.AudioManager.Play("BloopSound");
        GameManager.Instance.Player.transform.position = gameObject.GetComponent<Transform>().position;
        GameObject.Find("FishingPod").GetComponent<SpriteRenderer>().enabled = true;
        GameManager.Instance.Player.GetComponent<Animator>().SetBool("fishing",true);
        yield return new WaitForSeconds(Random.Range(0.7f, 4.5f));
        
        if (Random.Range(0f, 100f) <= probabilityForEnemyToAppear)
        {
            GameManager.Instance.CurrentEnemy = enemiesToAppear[Random.Range(0, enemiesToAppear.Count)];
            GameManager.Instance.StartBattleScene();
            StopCoroutine(Fishing());
        }
        else
        {
            GameManager.Instance.Player.GetComponent<Animator>().SetBool("fishing",false);
            GameObject.Find("FishingPod").GetComponent<SpriteRenderer>().enabled = false;
            GameManager.Instance.AudioManager.Play("SplashSound");
            GameManager.Instance.EnablePlayerMovement();
            GameManager.Instance.IsInteractActive = true;
            giveItem(_giveItem);
            Npc npc = GetComponent<Npc>();
            npc.StartDialogue();
            _isActive = false;
        }
    }

    private void giveItem(Item item)
    {
        var target = GameManager.Instance.Player.Backpack.Items.Find(x => x.Id == item.Id);
        if (target != null)
        {
            if (item.Quantity < 99)
            {
                item.Quantity += 1;
                EventManager.OnItemTaken?.Invoke(item);
            }
            else
            {
                Debug.Log("Inventario lleno");
            }
        }
        else
        {
            if (GameManager.Instance.Player.Backpack.Items.Count + 1 <=
                GameManager.Instance.Player.Backpack.NumSlots)
            {
                item.Quantity += 1;
                GameManager.Instance.Player.Backpack.AddItem(item);
                EventManager.OnItemTaken?.Invoke(item);
            }
            else
            {
                Debug.Log("Inventario lleno");
            }
        }
    }
}
