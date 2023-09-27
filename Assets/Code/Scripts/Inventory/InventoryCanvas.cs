using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private List<GameObject> optionButtons;
    [SerializeField] private GameObject questMenu;
    [SerializeField] private GameObject questContent;
    [SerializeField] private GameObject itemsMenu;
    [SerializeField] private GameObject itemsContent;
    [SerializeField] private GameObject itemDescriptionMenu;
    [SerializeField] private List<GameObject> itemDescriptionButtons;
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private List<GameObject> itemSlots;
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private TextMeshProUGUI healthQuantityText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemTitle;
    [SerializeField] private TMP_FontAsset inventoryFontAsset;
    
    [SerializeField] private Image itemImage;

    private UIInput _uiInput;

    private void OnEnable()
    {
        _uiInput.Enable();
        EventManager.OnItemTaken += ItemTaken;
    }

    private void OnDestroy()
    {
        _uiInput.Disable();
        EventManager.OnItemTaken -= ItemTaken;
    }

    private void Awake()
    {
        _uiInput = new UIInput();
    }
    
    private void Start()
    {
        title.SetActive(false);
        optionsMenu.SetActive(false);
        playerMenu.SetActive(false);
        questMenu.SetActive(false);
        itemsMenu.SetActive(false);
        itemDescriptionMenu.SetActive(false);
        GetCollectedItems();
        UpdateQuestInventory();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameSoftPaused)
        {
            OpenCloseInventory();
            if (optionsMenu.activeSelf && _uiInput.Inventory.Interact.WasPressedThisFrame())
            {
                ShowQuestMenu();
                ShowItemsMenu();
                ShowItemDescriptionMenu();
                ExitGame();
            }
        }
    }

    private void ExitGame()
    {
        if (EventSystem.current.currentSelectedGameObject == optionButtons[2])
        {
            
            CloseInventory();
            EventManager.OnSceneChange("MainMenu");
        }
    }

    private void OpenCloseInventory()
    {
        if (GameManager.Instance.OnOpenCloseInventory())
        {
            if (optionsMenu.activeSelf)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void CloseInventory()
    {
        GameManager.Instance.IsMenuActive = false;
        title.SetActive(false);
        questMenu.SetActive(false);
        itemsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        playerMenu.SetActive(false);
        itemDescriptionMenu.SetActive(false);
        GameManager.Instance.ResumeGame();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OpenInventory()
    {
        GameManager.Instance.IsMenuActive = true;
        GameManager.Instance.PauseGame();
        ShowPlayerMenuInfo();
        optionsMenu.SetActive(true);
        playerMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionButtons[0]);
    }

    private void ShowQuestMenu()
    {
        if (EventSystem.current.currentSelectedGameObject == optionButtons[0])
        {
            if (questMenu.activeSelf)
            {
                title.SetActive(false);
                questMenu.SetActive(false);
                CleanQuestInventory();
            }
            else
            {
                UpdateQuestInventory();
                itemsMenu.SetActive(false);
                itemDescriptionMenu.SetActive(false);
                title.SetActive(true);
                title.GetComponentInChildren<TextMeshProUGUI>().text = "Quests";
                questMenu.SetActive(true);
            }
        }
    }

    private void UpdateQuestInventory()
    {
        CleanQuestInventory();
        foreach (var quest in GameManager.Instance.Player.Backpack.Quests)
        {
            if (!quest.Completed)
                AssignQuestToInventory(quest);
        }
    }

    private void CleanQuestInventory()
    {
        if (questContent.transform.childCount != 0)
        {
            foreach (Transform child in questContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    

    private void AssignQuestToInventory(Quest quest)
    {
        var questName = new GameObject("QuestName");
        questName.transform.SetParent(questContent.transform);
        var questNameText = questName.AddComponent<TextMeshProUGUI>();
        questNameText.fontSize = 13;
        questNameText.rectTransform.sizeDelta = new Vector2(181.5f, 25f);
        questNameText.rectTransform.localScale = new Vector3(1, 1, 1);
        questNameText.text = quest.QuestName;
        questNameText.font = inventoryFontAsset;
        
        foreach (var goal in quest.Goals)
        {
            if (!goal.Completed && goal.IsGoalActive)
            {
                var goalGameObject = new GameObject("Goal");
                goalGameObject.transform.SetParent(questContent.transform);
                var goalText = goalGameObject.AddComponent<TextMeshProUGUI>();
                goalText.rectTransform.sizeDelta = new Vector2(181.5f, 25f);
                goalText.rectTransform.localScale = new Vector3(1, 1, 1);
                SetFontSize(goal.Description.ToCharArray(), goalText);
                goalText.text = goal.Description;
                goalText.font = inventoryFontAsset;
            }
        }
    }

    private void SetFontSize(char[] sentence, TextMeshProUGUI goalText)
    {
        if (sentence.Length <= 20)
        {
            goalText.fontSize = 11;
        }
        else if (sentence.Length > 20 && sentence.Length <= 50)
        {
            goalText.fontSize = 8;
        }
        else if (sentence.Length > 50)
        {
            goalText.fontSize = 6;
        }
    }

    private void ShowItemsMenu()
    {
        if (EventSystem.current.currentSelectedGameObject == optionButtons[1])
        {
            if (itemsMenu.activeSelf)
            {
                title.SetActive(false);
                itemsMenu.SetActive(false);
                CleanSlots();
            }
            else
            {
                AssignItemToSlot();
                questMenu.SetActive(false);
                itemDescriptionMenu.SetActive(false);
                title.GetComponentInChildren<TextMeshProUGUI>().text = "Items";
                title.SetActive(true);
                itemsMenu.SetActive(true);
            }
        }
    }
    
    private void GetCollectedItems()
    {
        var collectedItems = GameManager.Instance.Player.Backpack.Items;

        // Instanciar los slots
        var numSlots =  GameManager.Instance.Player.Backpack.NumSlots;
        for (int i = 0; i < numSlots; i++)
        {
            var target = Instantiate(itemSlot, itemsContent.transform);
            itemSlots.Add(target);
        }
        
        if (collectedItems.Count == 0) return;
        
        AssignItemToSlot();
    }

    private void AssignItemToSlot()
    {
        CleanSlots();
        var collectedItems = GameManager.Instance.Player.Backpack.Items;
        // Asignar objetos
        var orderedItemsList = collectedItems.OrderBy(x => x.Id).ToList();
        for (int i = 0; i < orderedItemsList.Count; i++)
        {
            if (orderedItemsList[i].Quantity > 0)
            {
                itemSlots[i].SetActive(true);
                itemSlots[i].GetComponent<ItemButton>().item = orderedItemsList[i];
                itemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = itemSlots[i].GetComponent<ItemButton>().item.Quantity.ToString();
                itemSlots[i].GetComponent<Image>().sprite = itemSlots[i].GetComponent<ItemButton>().item.Icon;
            }
        }
    }

    private void CleanSlots()
    {
        var numSlots = GameManager.Instance.Player.Backpack.NumSlots;
        if (numSlots > itemSlots.Count)
        {
            GetCollectedItems();
        }
        for (int i = 0; i < numSlots; i++)
        {
            itemSlots[i].GetComponent<ItemButton>().item = null;
            itemSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            itemSlots[i].GetComponent<Image>().sprite = null;
            itemSlots[i].SetActive(false);
        }
    }
    
    private void ShowItemDescriptionMenu()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == itemsContent)
        {
            var target = EventSystem.current.currentSelectedGameObject;
            if (itemDescriptionMenu.activeSelf)
            {
                itemDescriptionMenu.SetActive(false);
                EventSystem.current.SetSelectedGameObject(itemSlots[0]);
            }
            else
            {
                var item = target.GetComponent<ItemButton>().item;
                itemImage.sprite = item.Icon;
                itemDescription.text = item.Description;
                itemTitle.text = item.DisplayName;
                
                itemsMenu.SetActive(false);
                itemDescriptionMenu.SetActive(true);


                StartCoroutine(SelectOptionItemMenu(target));
            }
        }
    }

    private IEnumerator SelectOptionItemMenu(GameObject itemSlot)
    {
        // Activar los botones segun sea el caso
        var target = itemSlot.GetComponent<ItemButton>().item;
        itemDescriptionButtons[0].SetActive(target.IsUsable);
        itemDescriptionButtons[1].SetActive(target.IsDroppable);
        
        // Seleccionar el boton adecuado segun sea el caso
        if (target.IsUsable)
        {
            EventSystem.current.SetSelectedGameObject(itemDescriptionButtons[0]);
        }
        else if (target.IsDroppable)
        {
            EventSystem.current.SetSelectedGameObject(itemDescriptionButtons[1]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(optionButtons[1]);
        }

        yield return new WaitForSeconds(0f);
        while (itemDescriptionMenu.activeSelf)
        {
            if (_uiInput.Inventory.Interact.WasPressedThisFrame())
            {
                if (EventSystem.current.currentSelectedGameObject == itemDescriptionButtons[0])
                {
                    // Use
                    itemSlot.GetComponent<ItemButton>().item.Use();
                    itemDescriptionMenu.SetActive(false);
                    itemsMenu.SetActive(true);
                    AssignItemToSlot();
                    EventSystem.current.SetSelectedGameObject(itemsContent.transform.GetChild(0).gameObject);
                    ShowPlayerMenuInfo();
                    break;
                }
            
                if (EventSystem.current.currentSelectedGameObject == itemDescriptionButtons[1])
                {
                    // Delete
                    itemSlot.GetComponent<ItemButton>().item.Remove();
                    itemDescriptionMenu.SetActive(false);
                    itemsMenu.SetActive(true);
                    AssignItemToSlot();
                    EventSystem.current.SetSelectedGameObject(itemsContent.transform.GetChild(0).gameObject);
                    ShowPlayerMenuInfo();
                    break;
                }
            }
            yield return null;
        }
    }

    private void ShowPlayerMenuInfo()
    {
        var playerHealth = GameManager.Instance.Player.PlayerCombat;
        var itemQuantity = GameManager.Instance.Player.Backpack;
        healthQuantityText.text = $"HP {playerHealth.Health}/{playerHealth.MaxHealth}";
        itemQuantityText.text = $"Items {itemQuantity.Items.Count}/{itemQuantity.NumSlots}";
    }

    private void ItemTaken(Item item)
    {
        AssignItemToSlot();
    }
}
