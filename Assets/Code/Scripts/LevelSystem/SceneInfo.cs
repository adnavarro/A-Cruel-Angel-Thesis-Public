using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneInfo", menuName = "SceneInfo")]
public class SceneInfo : ScriptableObject
{
    [SerializeField] private String sceneName;
    [SerializeField] private int sceneEnteredCount;

    public string SceneName
    {
        get => sceneName;
        set => sceneName = value;
    }

    public int SceneEnteredCount
    {
        get => sceneEnteredCount;
        set => sceneEnteredCount = value;
    }
}
