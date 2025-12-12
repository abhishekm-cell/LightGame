using System;

using System.Collections.Generic;

using UnityEngine;

public class LevelDataManager 

{

    private const string SAVE_KEY = "LEVEL_DATA_SAVE";
    public LevelDataSaveList levelSaveDataList { get; private set; }

    public int totalLevels { get; private set; } = 30;

    private GameManager gameManager;
    public void SetReferece(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    #region SAVE & LOAD

    public void LoadData()

    {

        totalLevels = gameManager.GetLevelData();

        if (!PlayerPrefs.HasKey(SAVE_KEY))

        {

            CreateDefaultData();

            SaveData();

            return;

        }



        string json = PlayerPrefs.GetString(SAVE_KEY);



        try

        {

            levelSaveDataList = JsonUtility.FromJson<LevelDataSaveList>(json);

        }

        catch

        {

            Debug.LogWarning("Save corrupted → Resetting...");

            CreateDefaultData();

            SaveData();
        }

    }



    public void SaveData()

    {

        string json = JsonUtility.ToJson(levelSaveDataList, true);

        PlayerPrefs.SetString(SAVE_KEY, json);

        PlayerPrefs.Save();

    }



    private void CreateDefaultData()

    {

        levelSaveDataList = new LevelDataSaveList();

        levelSaveDataList.levelDataSave = new List<LevelDataSave>();



        for (int i = 1; i <= totalLevels; i++)

        {

            levelSaveDataList.levelDataSave.Add(new LevelDataSave

            {

                level = i,

                isUnlocked = (i == 1), // Only level 1 is unlocked initially

                stars = 0

            });

        }

    }



    #endregion



    #region UPDATE DATA



    public void UnlockNextLevel(int currentLevel)

    {

        int index = currentLevel; // since 1-based level, 2 = index 1

        Debug.Log("Unlocking level " + index+ "level"+ currentLevel);

        if (index < levelSaveDataList.levelDataSave.Count)

        {

            var l = levelSaveDataList.levelDataSave[index];

            l.isUnlocked = true;

            levelSaveDataList.levelDataSave[index] = l;

            SaveData();

        }

    }



    public void SetStars(int level, int stars)

    {

        Debug.Log("Setting stars for level " + level + " to " + stars);

        int index = level - 1;



        var l = levelSaveDataList.levelDataSave[index];

        l.stars = Math.Max(l.stars, stars); // never decrease stars

        levelSaveDataList.levelDataSave[index] = l;



        SaveData();

    }

    public LevelDataSave GetCurrentLevelData(int level)
    {
        return levelSaveDataList.levelDataSave[level - 1];
    }
    #endregion

}





[Serializable]

public class LevelDataSaveList

{

    public List<LevelDataSave> levelDataSave;

}



[Serializable]

public struct LevelDataSave

{

    public int level;

    public bool isUnlocked;

    public int stars;

}