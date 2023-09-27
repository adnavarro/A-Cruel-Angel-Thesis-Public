using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData<T>(T obj)
    {
        var path = Application.persistentDataPath + "/saves/";
        switch (obj)
        {
            case Player player:
                var playerData = new PlayerData(player);
                SaveSystem.SaveData(playerData, "/playerData.bin",path);
                break;
            case Item item:
                var itemData = new ItemData(item);
                SaveSystem.SaveData(itemData, $"/{item.DisplayName}.bin",path);
                break;
            case Quest quest:
                var questData = new QuestData(quest);
                SaveSystem.SaveData(questData, $"/quest{quest.Id}.bin",path);
                break;
            case Goal goal:
                var goalData = new GoalData(goal);
                SaveSystem.SaveData(goalData, $"/goal{goal.Id}.bin",path);
                break;
        }
    }

    public static void LoadData()
    {
        var path = Application.persistentDataPath + "/saves/";
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        foreach (var file in directoryInfo.GetFiles("*.bin"))
        {
            var filePath = file.ToString();
            var dataToLoad = SaveSystem.LoadData(filePath);
            switch (dataToLoad)
            {
                case PlayerData playerData:
                    LoadLastData(playerData);
                    break;
                case ItemData itemData:
                    LoadItemData(itemData);
                    break;
                case QuestData questData:
                    LoadQuestData(questData);
                    break;
                case GoalData goalData:
                    LoadGoalData(goalData);
                    break;
            }
        }
    }

    private static void LoadItemData(ItemData itemData)
    {
        foreach (var itemToLoad in GameManager.Instance.ItemsToSave)
        {
            if (itemToLoad.Id == itemData.Id)
            {
                itemToLoad.Quantity = itemData.Quantity;
                break;
            }
        }
    }

    private static void LoadQuestData(QuestData questData)
    {
        foreach (var questToLoad in GameManager.Instance.QuestsToSave)
        {
            if (questToLoad.Id == questData.Id)
            {
                questToLoad.Completed = questData.Completed;
                break;
            }
        }
    }

    private static void LoadGoalData(GoalData goalData)
    {
        foreach (var goalToLoad in GameManager.Instance.GoalsToSave)
        {
            if (goalToLoad.Id == goalData.Id)
            {
                goalToLoad.Completed = goalData.Completed;
                goalData.IsGoalActive = goalData.IsGoalActive;
                break;
            }
        }
    }

    private static void LoadLastData(PlayerData playerData)
    {
        EventManager.OnSceneLoaded += delegate { LoadPlayerData(playerData); };
        GameManager.Instance.WaitForSceneToLoad(playerData.PlayerLocationScene);
        EventManager.OnSceneChange?.Invoke(playerData.PlayerLocationScene);
    }

    private static void LoadPlayerData(PlayerData playerData)
    {
        Player player = GameManager.Instance.Player;
        player.PlayerCombat.Health = playerData.Health;
        player.PlayerCombat.MaxHealth = playerData.MaxHealth;
        player.Backpack.NumSlots = playerData.BackpackSlots;
        
        foreach (var itemId in playerData.ItemsIds)
        {
            var item = GameManager.Instance.ItemsToSave.Find(x => x.Id == itemId);
            if (!player.Backpack.Items.Contains(item))
            {
                player.Backpack.Items.Add(item);
            }
        }

        foreach (var questId in playerData.QuestsId)
        {
            var quest = GameManager.Instance.QuestsToSave.Find(x => x.Id == questId);
            if (!player.Backpack.Quests.Contains(quest))
            {
                player.Backpack.Quests.Add(quest);
            }
        }
        
        Vector3 position;
        position.x = playerData.Position[0];
        position.y = playerData.Position[1];
        position.z = playerData.Position[2];
        player.transform.position = position;
        EventManager.OnSceneLoaded -= delegate { LoadPlayerData(playerData); };
    }
}
