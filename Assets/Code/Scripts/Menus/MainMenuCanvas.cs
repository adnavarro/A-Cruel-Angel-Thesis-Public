using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private GameObject loadingText;
    

    private void Start()
    {
        GameManager.Instance.IsMenuActive = true;
        GameManager.Instance.IsInteractActive = true;
        var path = Application.persistentDataPath + "/saves/playerData.bin";
        if (!File.Exists(path))
        {
            buttons[1].SetActive(false);
            EventSystem.current.SetSelectedGameObject(buttons[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(buttons[1]);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.IsMenuActive = false;
    }

    private void Update()
    {
        if (GameManager.Instance.OnInteract("ui"))
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[0])
            {
                loadingText.SetActive(true);
                StartCoroutine(StartNewGame());
            }
            else if (EventSystem.current.currentSelectedGameObject == buttons[1])
            {
                LoadGame();
            }
            else if (EventSystem.current.currentSelectedGameObject == buttons[2])
            {
                ExitGame();
            }
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartNewGame()
    {
        yield return null;
        GameManager.Instance.ResetData();
        EventManager.OnSceneChange?.Invoke("SnowMap_1");
    }

    private void LoadGame()
    {
        GameManager.Instance.LoadData();
    }
}
