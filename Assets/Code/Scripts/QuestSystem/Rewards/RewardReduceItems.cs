using UnityEngine;

[CreateAssetMenu(fileName = "New ReduceItem-Reward", menuName = "ReduceItem-Reward")]
public class RewardReduceItems : Reward
{
    [SerializeField] private int itemId;
    [SerializeField] private int quantityToReduce;
    
    public override void GiveReward()
    {
        var itemToReduce = GameManager.Instance.Player.Backpack.Items.Find(x => x.Id == itemId);
        itemToReduce.Quantity -= quantityToReduce;
        if (itemToReduce.Quantity == 0)
        {
            GameManager.Instance.Player.Backpack.Items.Remove(itemToReduce);
        }
    }
}
