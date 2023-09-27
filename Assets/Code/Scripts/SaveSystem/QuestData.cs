[System.Serializable]
public class QuestData
{
    private int _id;
    private int[] _goalsId;
    private bool _completed;

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int[] GoalsId
    {
        get => _goalsId;
        set => _goalsId = value;
    }

    public bool Completed
    {
        get => _completed;
        set => _completed = value;
    }

    #endregion

    public QuestData(Quest quest)
    {
        Id = quest.Id;
        Completed = quest.Completed;
        var goals = quest.Goals;
        GoalsId = new int[goals.Count];
        for (int i = 0; i < goals.Count; i++)
        {
            GoalsId[i] = goals[i].Id;
        }
    }
}
