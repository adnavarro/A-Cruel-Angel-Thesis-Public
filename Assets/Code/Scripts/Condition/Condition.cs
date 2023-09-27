using UnityEngine;

public abstract class Condition : ScriptableObject
{
    public abstract int Id { get;}
    public abstract bool Completed { get; set; }
    public abstract string Description { get; set; }
    public abstract bool EvaluateCondition();
}
