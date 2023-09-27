using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Item> itemsToSave;
    [SerializeField] private List<Quest> questsToSave;
    [SerializeField] private List<Goal> goalsToSave;
    private static string _priorScene;
    private UIInput _uiInput;
    private NpcInput _npcInput;

    #region Properties
    
    public NpcInput NpcInput
    {
        get => _npcInput;
        set => _npcInput = value;
    }
    
    public static GameManager Instance { get; private set; }
    
    public Enemy CurrentEnemy { get; set; }
    
    public bool IsMenuActive { get; set; }
    
    public bool IsInteractActive { get; set; }
    
    public bool IsOpenCloseInventoryActive { get; set; }

    public PlayerCombat PlayerCombat { get; private set; }

    public AudioManager AudioManager
    {
        get
        {
            try
            {
                return FindObjectOfType<AudioManager>();
            }
            catch (Exception e)
            {
                Debug.Log(e + " There is no AudioManager GameObject in the scene");
                return null;
            }
        }
    }
    
    public UIInput UIInput
    {
        get => _uiInput;
        set => _uiInput = value;
    }
    
    public Player Player
    {
        get
        {
            try
            {
                return FindObjectOfType<Player>();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }
    }

    public bool IsGameSoftPaused { get; set; }

    public List<Item> ItemsToSave => itemsToSave;

    public List<Quest> QuestsToSave => questsToSave;

    public List<Goal> GoalsToSave => goalsToSave;

    #endregion

    private void Update()
    {
        if (IsMenuActive)
        {
            if (UIInput.Inventory.Move.WasPressedThisFrame())
            {
                AudioManager.Play("Menu Sound");
            }
        }
    }

    private void OnEnable()
    {
        NpcInput.Enable();
        UIInput.Enable();
    }

    private void OnDisable()
    {
        NpcInput.Disable();
        UIInput.Disable();
    }
    
    private void Awake()
    {
        IsGameSoftPaused = false;
        UIInput = new UIInput();
        NpcInput = new NpcInput();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IsInteractActive = true;
        IsOpenCloseInventoryActive = true;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        IsInteractActive = false;
        EnemyGenerator.IsGeneratorPaused = true;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsInteractActive = true;
        EnemyGenerator.IsGeneratorPaused = false;
    }

    #region SaveSystem

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        foreach (var item in itemsToSave)
        {
            item.ResetItem();
        }

        foreach (var goal in goalsToSave)
        {
            goal.ResetGoal();
        }
        
        foreach (var quest in QuestsToSave)
        {
            quest.ResetQuest();
        }
    }

    public void SaveData()
    {
        foreach (var item in itemsToSave)
        {
            SaveManager.SaveData(item);
        }

        foreach (var goal in goalsToSave)
        {
            SaveManager.SaveData(goal);
        }
        
        foreach (var quest in QuestsToSave)
        {
            SaveManager.SaveData(quest);
        }
        SaveManager.SaveData(Player);
        Debug.Log("Data Saved");
    }

    public void LoadData()
    {
        SaveManager.LoadData();
    }

    #endregion

    #region SceneSystem
    
    public void StartBattleScene()
    {
        Player.PlayerMovement.DisableMovement();
        Player.SavePlayerPosition();
        IsOpenCloseInventoryActive = false;
        IsInteractActive = false;
        PlayerCombat = Player.PlayerCombat;
        _priorScene = SceneManager.GetActiveScene().name;
        EventManager.OnSceneChange?.Invoke("CombatScene");
    }

    public void LeaveBattleScene()
    {
        IsMenuActive = false;
        EventManager.OnSceneLoaded += OnBattleSceneLeft;
        EventManager.OnSceneChange?.Invoke(_priorScene);
        if (SceneManager.GetActiveScene().name != _priorScene)
        {
            StartCoroutine(SceneLoader.WaitForSceneToLoad(_priorScene));
        }
    }
    
    private void OnBattleSceneLeft()
    {
        IsInteractActive = true;
        IsOpenCloseInventoryActive = true;
        Player.LoadPlayerPosition();
        EventManager.OnSceneLoaded -= OnBattleSceneLeft;
        CurrentEnemy.IncreaseEncounterCount();
        
        if (CurrentEnemy.EncounterCount() <= 1)
        {
            SaveData();
        }
    }

    public void WaitForSceneToLoad(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            StartCoroutine(SceneLoader.WaitForSceneToLoad(sceneName));
        }
    }

    #endregion

    #region InputSystem

    public void DeactivateMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisablePlayerMovement()
    {
        Player.PlayerMovement.DisableMovement();
    }

    public void EnablePlayerMovement()
    {
        Player.PlayerMovement.EnableMovement();
    }

    public bool OnOpenCloseInventory()
    {
        if (!IsOpenCloseInventoryActive)
        {
            return false;
        }

        if (UIInput.Inventory.Open_Close.WasPressedThisFrame())
        {
            AudioManager.Play("Ui effect");
            return true;
        }

        return false;
    }

    public bool OnInteract(string interactType)
    {
        if (!IsInteractActive)
            return false;
        
        if (interactType == "ui")
        {
            if (UIInput.UI.Interact.WasPressedThisFrame())
            {
                AudioManager.Play("Ui effect");
                return true;
            }
        }
        else if (interactType == "npc")
        {
            if (NpcInput.NPC.Interact.WasPressedThisFrame())
            {
                return true;
            }
        }
        else if (interactType == "inventory")
        {
            if (UIInput.Inventory.Interact.WasPressedThisFrame())
            {
                AudioManager.Play("Ui effect");
                return true;
            }
        }
        return false;
    }

    #endregion
}
