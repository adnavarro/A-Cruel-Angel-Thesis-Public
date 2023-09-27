using UnityEngine;

[CreateAssetMenu(fileName = "New Npc Goal", menuName = "Npc Goal")]
public class GoalTalkNpc : Goal
{
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private int npcId;
    [SerializeField] private bool completed;
    [SerializeField] private bool isGoalActive;
    [SerializeField] private Reward reward;
    [SerializeField] private int nextDialogueId;
    

    #region Properties
    
    public override bool IsGoalActive
    {
        get => isGoalActive;
        set => isGoalActive = value;
    }

    public override string Description => description;

    public override bool Completed
    {
        get => completed;
        set => completed = value;
    }

    public override int Id => id;

    public override Reward Reward => reward;

    #endregion

    private void OnEnable()
    {
        InitializeGoal();
    }
    
    private void OnDestroy()
    {
        EventManager.OnNpcDialogue -= EvaluateGoal;
    }

    public override void InitializeGoal()
    {
        EventManager.OnNpcDialogue += EvaluateGoal;
    }
    
    public void EvaluateGoal(Npc npc)
    {
        if (!Completed && IsGoalActive)
        {
            if (npc.Id == npcId)
            {
                CompletedGoal();
                npc.UpdateDialogue(nextDialogueId);
            }
        }
    }

    public override void CompletedGoal()
    {
        Completed = true;
        IsGoalActive = false;
        EventManager.OnGoalCompleted?.Invoke(this);
    }
    
    public override void ResetGoal()
    {
        IsGoalActive = true;
        Completed = false;
    }
}
