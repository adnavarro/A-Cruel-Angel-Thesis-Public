using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    [FormerlySerializedAs("_sceneTransition")] [SerializeField] private Animator sceneTransition;
    private static string _priorScene;

    public static string PriorScene
    {
        get => _priorScene;
        set => _priorScene = value;
    }

    private void Start()
    {
        SetSceneProperties();
    }

    private void OnEnable()
    {
        EventManager.OnSceneChange += SceneTransition;
    }
    
    private void OnDisable()
    {
        EventManager.OnSceneChange -= SceneTransition;
    }
    
    private static void SetSceneProperties()
    {
        GameManager.Instance.IsInteractActive = true;
        GameManager.Instance.IsOpenCloseInventoryActive = true;
        
        switch (SceneManager.GetActiveScene().name)
        {
            case "TestScene":
                CameraMotor.MaxPosition = new Vector2(8.9f, 0.45f);
                CameraMotor.MinPosition = new Vector2(1.47f, -10.06f);
                break;
            case "SnowMap_1":
                CameraMotor.MaxPosition = new Vector2(6.75f, -4.70f);
                CameraMotor.MinPosition = new Vector2(6.27f, -5.30f);
                break;
            case "SnowMap_2":
                CameraMotor.MaxPosition = new Vector2(14.75f, -5.68f);
                CameraMotor.MinPosition = new Vector2(7.29f, -16.32f);
                break;
            case "SnowMap_3":
                CameraMotor.MaxPosition = new Vector2(13.75f, -4.7f);
                CameraMotor.MinPosition = new Vector2(6.25f, -15.0f);
                break;
            case "SnowMap_4":
                CameraMotor.MaxPosition = new Vector2(7.27f, -4.70f);
                CameraMotor.MinPosition = new Vector2(7.27f, -5.30f);
                break;
            case "SnowMap_5":
                CameraMotor.MaxPosition = new Vector2(6.7f, -4.7f);
                CameraMotor.MinPosition = new Vector2(6.7f, -4.7f);
                break;
            case "SnowMap_6":
                CameraMotor.MaxPosition = new Vector2(23.72f, -4.72f);
                CameraMotor.MinPosition = new Vector2(6.22f, -25.31f);
                break;
            case "Bar":
                CameraMotor.MaxPosition = new Vector2(6.7f, -4.7f);
                CameraMotor.MinPosition = new Vector2(6.7f, -4.7f);
                break;
        }
    }

    private void SceneTransition(string sceneName)
    {
        PriorScene = SceneManager.GetActiveScene().name;
        StartCoroutine(ActivateAnimation(sceneName));
    }
    
    private IEnumerator ActivateAnimation(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
    
    public static IEnumerator WaitForSceneToLoad(string sceneName)
    {
        PriorScene = SceneManager.GetActiveScene().name;
        
        while (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return null;
        }

        if (SceneManager.GetActiveScene().name == sceneName)
        {
            EventManager.OnSceneLoaded?.Invoke();
        }
    }
}
