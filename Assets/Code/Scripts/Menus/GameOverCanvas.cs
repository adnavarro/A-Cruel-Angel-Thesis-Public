using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;

    
    private void Start()
    {
        GameManager.Instance.IsMenuActive = true;
        EventSystem.current.SetSelectedGameObject(buttons[0]);
        var path = Application.persistentDataPath + "/saves/playerData.bin";
        if (!File.Exists(path))
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(true);
        }
        else
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.IsMenuActive = false;
        GameManager.Instance.IsOpenCloseInventoryActive = true;
    }

    private void Update()
    {
        if (GameManager.Instance.OnInteract("ui"))
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[0])
            {
                LoadGame();
            }
            else if (EventSystem.current.currentSelectedGameObject == buttons[1])
            {
                StartNewGame();
            }
        }
    }

    private void LoadGame()
    {
        GameManager.Instance.LoadData();
    }
    
    private void StartNewGame()
    {
        GameManager.Instance.ResetData();
        EventManager.OnSceneChange?.Invoke("SnowMap_1");
    }
}
