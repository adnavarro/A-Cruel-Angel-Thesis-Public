using UnityEngine;

[CreateAssetMenu(fileName = "New Condition - IsSceneEntered", menuName = "ConditionIsSceneEntered")]
public class ConditionIsSceneEntered : Condition
{
    [SerializeField] private int id;
    [SerializeField] private string sceneName;
    [SerializeField] private int enteredCountCheck;
    
    public override int Id => id;
    public override bool Completed { get; set; }
    public override string Description { get; set; }
    
    public override bool EvaluateCondition()
    {
        if (FindObjectOfType<LevelManager>().GetSceneEnteredCountByName(sceneName) >= enteredCountCheck)
        {
            return Completed = true;
        }

        return Completed = false;
    }
}
