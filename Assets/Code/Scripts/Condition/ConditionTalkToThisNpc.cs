using UnityEngine;

[CreateAssetMenu(fileName = "New Condition - TalkToThisNpc", menuName = "ConditionTalkToThisNpc")]
public class ConditionTalkToThisNpc : Condition
{
    [SerializeField] private int id;
    [SerializeField] private int npcId;
    [SerializeField] private int dialogueId;
    
    public override int Id => id;
    public override bool Completed { get; set; }
    public override string Description { get; set; }
    public int NpcId => npcId;

    public override bool EvaluateCondition()
    {
        if (PlayerPrefs.HasKey(npcId + "NpcDialogueIndex"))
        {
            var npcDialogueId = PlayerPrefs.GetInt(npcId + "NpcDialogueIndex");
            if (npcDialogueId >= dialogueId)
            {
                Completed = true;
            }
        }
        return Completed;
    }
}
