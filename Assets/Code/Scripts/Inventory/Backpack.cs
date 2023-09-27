using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Backpack", menuName = "Backpack")]
public class Backpack : ScriptableObject
{
    [SerializeField] private List<Item> items;
    [SerializeField] private List<Quest> quests;
    [SerializeField] private int numSlots;

    public List<Item> Items => items;
    public List<Quest> Quests => quests;

    public int NumSlots
    {
        get => numSlots;
        set => numSlots = value;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
    }
    
}