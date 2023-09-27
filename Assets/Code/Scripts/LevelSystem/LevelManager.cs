using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Condition conditionToObjectsDisappear;
    [SerializeField] private List<GameObject> objectsToDisappear;
    [SerializeField] private Condition conditionToObjectsAppear;
    [SerializeField] private List<GameObject> objectsToAppear;
    [SerializeField] private List<ConditionTalkToThisNpc> npcsToTalk;
    [SerializeField] private bool isThereOptionalNpcToTalk;
    [SerializeField] private SymptomAttached symptomAttached;
    private List<Npc> _npcsInScene;

    private void Awake()
    {
        IncreaseSceneEnteredCount(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        _npcsInScene = FindObjectsOfType<Npc>().ToList();
        AppearAndDisappearObjects();
        SetPlayerPosition();
    }

    public int GetSceneEnteredCount()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey(sceneName + "Info"))
        {
            var sceneCount = PlayerPrefs.GetInt(sceneName + "Info");
            return sceneCount;
        }

        return 0;
    }

    public int GetSceneEnteredCountByName(string sceneName)
    {
        if (PlayerPrefs.HasKey(sceneName + "Info"))
        {
            var sceneCount = PlayerPrefs.GetInt(sceneName + "Info");
            return sceneCount;
        }
        return 0;
    }

    private void IncreaseSceneEnteredCount(string sceneName)
    {
        if (PlayerPrefs.HasKey(sceneName + "Info"))
        {
            int sceneEnteredCount = PlayerPrefs.GetInt(sceneName + "Info");
            sceneEnteredCount++;
            PlayerPrefs.SetInt(sceneName + "Info", sceneEnteredCount);
        }
        else
        {
            int sceneEnteredCount = 1;
            PlayerPrefs.SetInt(sceneName + "Info", sceneEnteredCount);
        }
    }

    private void AppearAndDisappearObjects()
    {
        if (conditionToObjectsDisappear && objectsToDisappear != null)
        {
            if (conditionToObjectsDisappear.EvaluateCondition())
            {
                foreach (var target in objectsToDisappear)
                {
                    target.SetActive(false);
                }
            }
        }

        if (conditionToObjectsAppear && objectsToAppear!= null)
        {
            if (conditionToObjectsAppear.EvaluateCondition())
            {
                foreach (var target in objectsToAppear)
                {
                    target.SetActive(true);
                    if (target.CompareTag("Npc") && GetSceneEnteredCount() == 1)
                    {
                        _npcsInScene.Add(target.GetComponent<Npc>());
                    }
                }
            }
        }
    }

    private void SetPlayerPosition()
    {
        var priorScene = SceneLoader.PriorScene;
        var player = GameManager.Instance.Player;
        if (SceneManager.GetActiveScene().name == "SnowMap_1")
        {
            switch (priorScene)
            {
                case "SnowMap_2":
                    player.transform.position = new Vector3(12, -4.5f, 0);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "SnowMap_2")
        {
            switch (priorScene)
            {
                case "SnowMap_1":
                    player.transform.position = new Vector3(2.72f, -12.07f, 0);
                    break;
                case "SnowMap_4":
                    player.transform.position = new Vector3(19, -11.8f, 0);
                    break;
                case "SnowMap_3":
                    player.transform.position = new Vector3(11f, -2.09f, 0);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "SnowMap_3")
        {
            switch (priorScene)
            {
                case "SnowMap_2":
                    player.transform.position = new Vector3(10.04f, -18.45f, 0);
                    break;
                case "SnowMap_5":
                    player.transform.position = new Vector3(18.2f, -9.83f, 0);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "SnowMap_5")
        {
            switch (priorScene)
            {
                case "SnowMap_6":
                    player.transform.position = new Vector3(6.37f, -1.12f, 0);
                    break;
                case "SnowMap_3":
                    player.transform.position = new Vector3(1.3f, -6.83f, 0);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "SnowMap_6")
        {
            switch (priorScene)
            {
                case "SnowMap_5":
                    player.transform.position = new Vector3(19.51f, -28.5f, 0);
                    break;
                case "Bar":
                    player.transform.position = new Vector3(15.43f, -13.53f, 0);
                    break;
            }
        }
    }
}
