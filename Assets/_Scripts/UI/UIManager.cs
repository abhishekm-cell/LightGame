using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private LevelSelectPanelUIController levelSelectPanelUIController;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private MainMenu mainMenu;

     private GameManager gameManager;
    public void SetReferece(GameManager gameManager)
    {
        this.gameManager = gameManager;

        gameUI.SetRefrence(this);
        mainMenu.SetRefrence(this);
    }

    public void RestartLevel()
    {
        gameManager.GetLevelManager().RestartLevel();
    }
    public int GetCurrentLevelIndex()=> gameManager.GetLevelManager().currentLevelIndex;

    public void LoadLevel(int level)
    {
        
    }

    public List<LevelDataSave> GetLevelData()
    {
        return gameManager.GetLevelDataManager().levelSaveDataList.levelDataSave;
    }
}