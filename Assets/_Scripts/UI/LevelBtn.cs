using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{ 
    [SerializeField] private Button levelLoadbtn;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject lockPanel;
    [Header("starts")]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private LevelDataSave levelData;
    private LevelSelectPanelUIController levelSelectPanelUIController;

    public void SetConfig(LevelSelectPanelUIController levelSelectPanelUIController,LevelDataSave levelData)
    {
        this.levelSelectPanelUIController = levelSelectPanelUIController;
        this.levelData = levelData;
        Debug.Log(levelData.level + " " + levelData.isUnlocked + " " + levelData.stars);
        SetLock(!levelData.isUnlocked);
        SetStart(levelData.stars);
        UpdateLevelText();
    }
    private void Awake()
    {
        levelLoadbtn.onClick.AddListener(()=>{
            levelSelectPanelUIController.LoadLevel(levelData.level);
        });
    }
    private void SetLock(bool locked)
    {
        lockPanel.SetActive(locked);
        levelLoadbtn.interactable = !locked;
    }
    private void UpdateLevelText()
    {
        levelText.text = ""+levelData.level;
    }

    private void SetStart(int starts)
    {
        star1.SetActive(starts >= 1);
        star2.SetActive(starts >= 2);
        star3.SetActive(starts >= 3);
    }
}