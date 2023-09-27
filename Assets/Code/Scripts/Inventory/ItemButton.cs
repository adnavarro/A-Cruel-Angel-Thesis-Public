using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private int id;
    private int _itemCount;
    private Sprite _icon;
    public Item item;

    public Sprite Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }

    public int Id
    {
        get => id;
        set => id = value;
    }
}
