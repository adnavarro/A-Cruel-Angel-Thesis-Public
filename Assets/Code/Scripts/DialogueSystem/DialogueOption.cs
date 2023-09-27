using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New DialogueOption", menuName = "DialogueOption")]

public class DialogueOption : ScriptableObject
{
    [SerializeField] private int npcId;
    [FormerlySerializedAs("Id")] [SerializeField] private int id;
    [SerializeField] private string buttonText;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private List<int> nextDialogueOptionIds;
    [SerializeField] private Condition condition;
    [SerializeField] private Quest quest;
    [SerializeField] private Item item;
    [SerializeField] private Reward reward;
    [SerializeField] private SymptomAttached symptomAttached;
    [SerializeField] private bool endsOnBattle;
    [SerializeField] private bool isSymptomQuestion;

    #region Properties

    public string ButtonText => buttonText;

    public Dialogue Dialogue
    {
        get => dialogue;
        set => dialogue = value;
    }
    
    public List<int> NextDialogueOptionIds => nextDialogueOptionIds;

    public Quest Quest => quest;

    public bool EndsOnBattle => endsOnBattle;

    public bool IsSymptomQuestion => isSymptomQuestion;

    public SymptomAttached SymptomAttached => symptomAttached;

    public Item Item => item;

    public Reward Reward => reward;

    public Condition Condition => condition;

    #endregion
}
