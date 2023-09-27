using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    private int _health;
    private int _maxHealth;
    private int _backpackSlots;
    private string _playerLocationScene;
    private float[] _position;
    private int[] _questsIds;
    private int[] _itemsIds;

    #region Properties

    public int Health
    {
        get => _health;
        set => _health = value;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public int BackpackSlots
    {
        get => _backpackSlots;
        set => _backpackSlots = value;
    }

    public string PlayerLocationScene
    {
        get => _playerLocationScene;
        set => _playerLocationScene = value;
    }

    public float[] Position
    {
        get => _position;
        set => _position = value;
    }

    public int[] QuestsId
    {
        get => _questsIds;
        set => _questsIds = value;
    }

    public int[] ItemsIds
    {
        get => _itemsIds;
        set => _itemsIds = value;
    }

    #endregion
    
    public PlayerData(Player player)
    {
        Health = player.PlayerCombat.Health;
        MaxHealth = player.PlayerCombat.MaxHealth;
        BackpackSlots = player.Backpack.NumSlots;
        PlayerLocationScene = SceneManager.GetActiveScene().name;
        
        Position = new float[3];
        var position = player.transform.position;
        Position[0] = position.x;
        Position[1] = position.y;
        Position[2] = position.z;
        
        var quests = player.Backpack.Quests;
        QuestsId = new int[quests.Count];
        for (int i = 0; i < quests.Count; i++)
        {
            QuestsId[i] = quests[i].Id;
        }

        var items = player.Backpack.Items;
        ItemsIds = new int[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            ItemsIds[i] = items[i].Id;
        }
    }
}
