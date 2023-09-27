[System.Serializable]
public class ItemData
{
    private int _id;
    private int _quantity;

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public ItemData(Item item)
    {
        Quantity = item.Quantity;
        Id = item.Id;
    }
}
