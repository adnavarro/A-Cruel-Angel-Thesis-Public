using UnityEngine;

[CreateAssetMenu(fileName = "New Condition - ItemInInventory", menuName = "ConditionIsItemInInventory")]
public class ConditionIsItemInInventory : Condition
{
    [SerializeField] private int id;
    [SerializeField] private Item item;
    [SerializeField] private int itemQuantityToCheck;

    public override int Id => id;
    public override bool Completed { get; set; }
    public override string Description { get; set; }
    
    public override bool EvaluateCondition()
    {
        var playerItems = GameManager.Instance.Player.Backpack.Items;
        foreach (var playerItem in playerItems)
        {
            if (playerItem == item)
            {
                if (itemQuantityToCheck <= playerItem.Quantity)
                {
                    return Completed = true;
                }
            }
        }

        return Completed = false;
    }
}
