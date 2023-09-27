using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityAction<Npc> OnNpcDialogue;
    public static UnityAction<Goal> OnGoalCompleted;
    public static UnityAction<Quest> OnQuestCompleted;
    public static UnityAction<Item> OnItemTaken;
    public static UnityAction<string> OnSceneChange; // El string es el nombre de la escena
    public static UnityAction OnSceneLoaded;
    public static UnityAction OnHealthChanged;
    public static UnityAction OnPortalStepped;
}