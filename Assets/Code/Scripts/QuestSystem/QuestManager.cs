using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest { get; set; }
    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        EventManager.OnQuestCompleted += CheckQuest;
    }

    private void Start()
    {
        GetCurrentToDo();
    }

    private void CheckQuest(Quest quest)
    {
        if (quest.Completed)
        {
            GetCurrentToDo();
        }
    }

    private void GetCurrentToDo()
    {
        if (GameManager.Instance.Player.Backpack.Quests.Count <= 0) return;
        foreach (var quest in GameManager.Instance.Player.Backpack.Quests)
        {
            if (quest.Completed) continue;
            CurrentQuest = quest;
            break;
        }
    }
}
