using UnityEngine;

[CreateAssetMenu(fileName = "New Condition - GoalCompleted", menuName = "ConditionGoalCompleted")]
public class ConditionGoalCompleted : Condition
{
    [SerializeField] private int id;
    [SerializeField] private Goal goal;

    public override int Id => id;
    
    public override string Description { get; set; }
    
    public override bool Completed { get; set; }
    
    public override bool EvaluateCondition()
    {
        Completed = goal.Completed;
        return Completed;
    }
}


