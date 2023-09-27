using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerCombat", menuName = "PlayerCombat")]
public class PlayerCombat : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public Sprite combatSprite;

    #region Properties
    
    public int Health
    {
        get => health;
        set => health = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    
    #endregion
}
