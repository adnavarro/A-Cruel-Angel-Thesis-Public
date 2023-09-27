using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string questName;
    [SerializeField] private List<Goal> goals;
    [SerializeField] private bool completed;

    #region GetSet
    
    public int Id => id;
    
    public string QuestName
    {
        get => questName;
        set => questName = value;
    }
    public List<Goal> Goals => goals;
    
    public bool Completed
    {
        get => completed;
        set => completed = value;
    }
    
    #endregion
    
    private void OnEnable()
    {
        EventManager.OnGoalCompleted += CheckGoal;
    }

    private void OnDestroy()
    {
        EventManager.OnGoalCompleted -= CheckGoal;
    }

    private void CheckGoal(Goal goal)
    {
        //Pasa por aquí varias veces por alguna razon
        if (!Completed)
        {
            if (goals.Exists(x => x.Id == goal.Id))
            {
                if (goals.All(g => g.Completed))
                {
                    Completed = true;
                }

                if (Completed)
                {
                    EventManager.OnQuestCompleted?.Invoke(this);
                    GameManager.Instance.Player.Backpack.Quests.Remove(this);
                }
                GameManager.Instance.SaveData();
            }
        }
    }

    public void ResetQuest()
    {
        Completed = false;
    }
}
