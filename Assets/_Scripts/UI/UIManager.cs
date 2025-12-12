using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private LevelSelectPanelUIController levelSelectPanelUIController;
    [SerializeField] private GameplayUIController gameUI;
    [SerializeField] private MainMenuUIController mainMenu;

     private GameManager gameManager;
    public void SetReferece(GameManager gameManager)
    {
        this.gameManager = gameManager;

        gameUI.SetRefrence(this);
        mainMenu.SetRefrence(this);
    }

    void OnEnable()
    {
        ActivateMainMenu();
    }
    public void RestartLevel()
    {
        gameManager.GetLevelManager().RestartLevel();
    }
    public int GetCurrentLevelIndex()=> gameManager.GetLevelManager().currentLevelIndex;

    public void StartGame(int level)
    {
        gameManager.StartGame(level);
    }

    public void LoadNextLevel()
    {
        gameManager.NextLevel();
    }

    public List<LevelDataSave> GetLevelData()
    {
        return gameManager.GetLevelDataManager().levelSaveDataList.levelDataSave;
    }

    public void ActivateMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    public void ActivateGameplayUI()
    {
        mainMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void ActivateLevelCompletePanel()
    {
        gameUI.ActivateLevelCompletePanel();
    }

    public int GetCurrentLevelStarts()
    {
        return gameManager.GetLevelDataManager().GetCurrentLevelData(gameManager.GetLevelManager().currentLevelIndex).stars;
    }
}