using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameplayUIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;

    [Header("References")]
    [SerializeField] private LevelCompleteUIController levelCompleteUIController;

    [Header("Text")]
    [SerializeField]private TextMeshProUGUI levelText;
    private UIManager uIManager;

    public void SetRefrence(UIManager uIManager)
    {
        this.uIManager = uIManager;
        levelCompleteUIController.SetRefrence(uIManager);
    }
    void Awake()
    {
        homeButton.onClick.AddListener(() => uIManager.ActivateMainMenu());
        restartButton.onClick.AddListener(() => uIManager.RestartLevel());
        levelCompleteUIController.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        levelCompleteUIController.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        levelCompleteUIController.gameObject.SetActive(false);
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

    public void ActivateLevelCompletePanel(int stars)
    {
        
        levelCompleteUIController.gameObject.SetActive(true);
        levelCompleteUIController.SetStart(stars);
    }

    public void DeactivateLevelCompletePanel()
    {
        levelCompleteUIController.gameObject.SetActive(false);
    }
}