using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Dazed", menuName = "Item Dazed")]
public class ItemDazeEffect : Item
{
    [SerializeField] private int id;
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int quantity;
    [SerializeField] private bool isDroppable;
    [SerializeField] private bool isUsable;
    [SerializeField] private float effectTime;
    
    private CameraMotor _cameraMotor;
    
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
        try
        {
            _cameraMotor = FindObjectOfType<CameraMotor>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
        _cameraMotor.SetEffectTime(effectTime);
        
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
