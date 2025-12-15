using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button levelSelectBtn;
    [SerializeField] private Button closeLevelSelectBtn;
    [SerializeField] private Button musicOnBtn;
    [SerializeField] private Button musicOffBtn;
    [SerializeField] private LevelSelectPanelUIController levelSelectPanelUIController;
    private UIManager uIManager;
    public void SetRefrence(UIManager uIManager)
    {
        this.uIManager = uIManager;
        levelSelectPanelUIController.SetRefrence(uIManager);
    }

    private void Awake()
    {
        levelSelectBtn.onClick.AddListener(() =>
        {
            levelSelectPanelUIController.gameObject.SetActive(true);
        });
        closeLevelSelectBtn.onClick.AddListener(() =>
        {
            levelSelectPanelUIController.gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        levelSelectPanelUIController.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        levelSelectPanelUIController.gameObject.SetActive(false);
    }

    private void MusicOn()
    {
        
    }

    private void MusicOff()
    {
        
    }
}