[System.Serializable]
public class GoalData
{
    private int _id;
    private bool _completed;
    private bool _isGoalActive;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public bool Completed
    {
        get => _completed;
        set => _completed = value;
    }

    public bool IsGoalActive
    {
        get => _isGoalActive;
        set => _isGoalActive = value;
    }

    public GoalData(Goal goal)
    {
        Id = goal.Id;
        Completed = goal.Completed;
        IsGoalActive = goal.IsGoalActive;
    }
}
