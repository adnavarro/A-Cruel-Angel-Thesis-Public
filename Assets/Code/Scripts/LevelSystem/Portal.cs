using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Goal goalToPass;
    [SerializeField] private Condition conditionToPass;
    [SerializeField] private string nextSceneName;
    private LevelManager _levelManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (goalToPass || conditionToPass)
            {
                if ((goalToPass && goalToPass.Completed) || (conditionToPass && conditionToPass.EvaluateCondition()))
                {
                    EventManager.OnSceneChange(nextSceneName);
                }
                else
                {
                    EventManager.OnPortalStepped?.Invoke();
                }
            }
            else
            {
                EventManager.OnSceneChange(nextSceneName);
            }
        }
    }
}
