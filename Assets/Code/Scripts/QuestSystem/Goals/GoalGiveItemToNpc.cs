using UnityEngine;

[CreateAssetMenu(fileName = "New Item-Npc Goal", menuName = "Item-Npc Goal")]
public class GoalGiveItemToNpc : Goal
{
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private int npcId;
    [SerializeField] private int itemId;
    [SerializeField] private int itemQuantity;
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
        if (!Completed && IsGoalActive && PlayerOwnedThisGoal())
        {
            if (npc.Id == npcId)
            {
                var player = GameManager.Instance.Player;
                var playerItems = player.Backpack.Items;
                var item = playerItems.Find(x => x.Id == itemId);
                if (item != null)
                {
                    if (item.Quantity >= itemQuantity)
                    {
                        CompletedGoal();
                        npc.UpdateDialogue(nextDialogueId);
                    }
                }
            }
        }
    }

    private bool PlayerOwnedThisGoal()
    {
        var target = GameManager.Instance.Player.Backpack.Quests.Exists(x => x.Goals.Find(y => y == this));
        if (target)
            return true;
        return false;
    }
    
    public override void CompletedGoal()
    {
        Completed = true;
        isGoalActive = false;
        reward.GiveReward();
        EventManager.OnGoalCompleted?.Invoke(this);
    }

    public override void ResetGoal()
    {
        IsGoalActive = true;
        Completed = false;
    }
}
