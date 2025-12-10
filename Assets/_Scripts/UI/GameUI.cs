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

    [Header("Text")]
    [SerializeField]private TextMeshProUGUI levelText;
    private UIManager uIManager;

    public void SetRefrence(UIManager uIManager)
    {
        this.uIManager = uIManager;
    }
    void Awake()
    {
        restartButton.onClick.AddListener(() => uIManager.RestartLevel());

        
    }
    void LateUpdate()
    {
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        //if(levelManager.allLevels.levels.Last() == levelManager.currentLevel)
        levelText.text = "Level " + uIManager.GetCurrentLevelIndex();
    }
}