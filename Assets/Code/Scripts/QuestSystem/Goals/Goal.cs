using UnityEngine;

public abstract class Goal: ScriptableObject
{
    public abstract int Id { get; }
    public abstract string Description { get; }
    public abstract bool Completed { get; set; }
    public abstract bool IsGoalActive { get; set; }
    public abstract Reward Reward { get; }
    public abstract void InitializeGoal();
    public abstract void CompletedGoal();
    public abstract void ResetGoal();
}
