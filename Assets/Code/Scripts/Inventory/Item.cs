using UnityEngine;

public abstract class Item : ScriptableObject
{
    public abstract string DisplayName { get; }
    public abstract string Description { get; }
    public abstract Sprite Icon { get; }
    public abstract bool IsDroppable { get; }
    public abstract bool IsUsable { get; }
    public abstract int Id { get; }
    public abstract int Quantity { get; set; }
    public abstract void Use();
    public abstract void Remove();
    public abstract void ResetItem();
}