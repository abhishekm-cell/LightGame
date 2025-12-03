using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    // [SerializeField] private Button nextLevelButton;
    // [SerializeField] private Button pauseButton;
    // [SerializeField] private Button backToMenuButton;

    [Header("References")]
    [SerializeField] private LevelManager levelManager;

    [Header("Text")]
    [SerializeField]private TextMeshProUGUI levelText;

    void Awake()
    {
        restartButton.onClick.AddListener(() => levelManager.RestartLevel());

        
    }
    void LateUpdate()
    {
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        if(levelManager.allLevels.levels.Last() == levelManager.currentLevel)
        levelText.text = "Level " + levelManager.currentLevelIndex;
    }
}