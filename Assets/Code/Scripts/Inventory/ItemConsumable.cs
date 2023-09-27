using UnityEngine;

[CreateAssetMenu(fileName = "New Item Consumable", menuName = "Item Consumable")]
public class ItemConsumable: Item
{
    [SerializeField] private int id;
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int quantity;
    [SerializeField] private int healthIncreaseAmount;
    [SerializeField] private bool isDroppable;
    [SerializeField] private bool isUsable;
    
    #region Properties
    
    public override string DisplayName => displayName;
    public override int Quantity
    {
        get => quantity; 
        set => quantity = value;
    }
    public override string Description => description;
    public override Sprite Icon => icon;
    public override bool IsDroppable => isDroppable;
    public override bool IsUsable => isUsable;
    public override int Id => id;

    #endregion
    
    public override void Use()
    {
        if (isUsable)
        {
            var playerHealth = GameManager.Instance.Player.PlayerCombat.Health;
            playerHealth += healthIncreaseAmount;
            if (playerHealth > GameManager.Instance.Player.PlayerCombat.MaxHealth)
            {
                GameManager.Instance.Player.PlayerCombat.Health = GameManager.Instance.Player.PlayerCombat.MaxHealth;
            }
            else
            {
                GameManager.Instance.Player.PlayerCombat.Health += healthIncreaseAmount;
            }

            foreach (var item in GameManager.Instance.Player.Backpack.Items)
            {
                if (item.Id == Id)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity -= 1;
                    }
                    else
                    {
                        item.Quantity = 0;
                        GameManager.Instance.Player.Backpack.Items.Remove(this);
                    }
                    break;
                }
            }
        }
    }

    public override void Remove()
    {
        if (isDroppable)
        {
            foreach (var item in GameManager.Instance.Player.Backpack.Items)
            {
                if (item.Id == Id)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity -= 1;
                    }
                    else
                    {
                        item.Quantity = 0;
                        GameManager.Instance.Player.Backpack.Items.Remove(this);
                    }

                    break;
                }
            }
        }
    }

    public override void ResetItem()
    {
        Quantity = 0;
    }
}
