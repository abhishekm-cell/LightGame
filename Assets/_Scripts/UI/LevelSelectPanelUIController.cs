using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanelUIController : MonoBehaviour
{
    [SerializeField] LevelBtn levelButtonPrefab;
    [SerializeField] private Transform levelButtonParent;
    [Header("UI")]
    [SerializeField] private Button closeBtn;
    private List<LevelDataSave> levelsData;

    private UIManager uIManager;
    private List<LevelBtn> levelButtonsList = new List<LevelBtn>(); // <> levelDataSave

    public void SetRefrence(UIManager uIManager)
    {   
        this.uIManager = uIManager;
    }

    private void Awake()
    {
        closeBtn.onClick.AddListener(() =>{ 
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        });
    }
    void Start()
    {
        levelsData =  uIManager.GetLevelData();
        foreach(LevelDataSave levelData in levelsData)
        {
            LevelBtn levelBtn =Instantiate<LevelBtn>(levelButtonPrefab,levelButtonParent);
            levelButtonsList.Add(levelBtn);
            levelBtn.SetConfig(this,levelData);
        }
    }
    void OnEnable()
    {
        levelsData =  uIManager.GetLevelData();
        for(int i = 0; i < levelButtonsList.Count; i++)
        {
            levelButtonsList[i].SetConfig(this,levelsData[i]);
        }
    }
    public void StartGame(int level)=>uIManager.StartGame(level);
}