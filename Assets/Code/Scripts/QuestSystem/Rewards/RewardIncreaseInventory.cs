using UnityEngine;

[CreateAssetMenu(fileName = "New IncreaseInventory-Reward", menuName = "IncreaseInventory-Reward")]
public class RewardIncreaseInventory : Reward
{
    [SerializeField] private int quantityToIncrease;

    public override void GiveReward()
    {
        GameManager.Instance.Player.Backpack.NumSlots += quantityToIncrease;
        GameManager.Instance.SaveData();
    }
}
