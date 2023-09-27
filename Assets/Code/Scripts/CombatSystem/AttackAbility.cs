using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Ability", menuName = "Ability")]
public class AttackAbility : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private int baseAttack;
    [SerializeField] private List<Dialogue> responses;

    #region Properties
    public string Name
    {
        get => name;
        set => name = value;
    }

    public int BaseAttack
    {
        get => baseAttack;
        set => baseAttack = value;
    }

    public List<Dialogue> Responses
    {
        get => responses;
        set => responses = value;
    }

    #endregion
}
