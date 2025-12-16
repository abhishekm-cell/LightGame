using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button levelSelectBtn;
    [SerializeField] private Button closeLevelSelectBtn;
    [SerializeField] private Button musicOnBtn;
    [SerializeField] private Button musicOffBtn;
    [SerializeField] private Button gameInfoBtn;
    [SerializeField] private Button closeInfoBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject musicIconOn,musicIconOff;
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
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        });
        closeLevelSelectBtn.onClick.AddListener(() =>
        {
            levelSelectPanelUIController.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        });

        gameInfoBtn.onClick.AddListener(() =>
        {
            infoPanel.SetActive(true);
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        });
        closeInfoBtn.onClick.AddListener(() =>
        {
            infoPanel.SetActive(false);
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
        });

        musicOnBtn.onClick.AddListener(MusicOn);
        musicOffBtn.onClick.AddListener(MusicOff);

        exitBtn.onClick.AddListener(OnQuit);

    }
    private void OnEnable()
    {
        levelSelectPanelUIController.gameObject.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void OnDisable()
    {
        levelSelectPanelUIController.gameObject.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void MusicOn()
    {
        Debug.Log("Music on");
        AudioManager.Instance.SetMusic(false);
        musicIconOff.SetActive(true);
        musicIconOn.SetActive(false);
    }

    private void MusicOff()
    {
        Debug.Log("Music off");
        AudioManager.Instance.SetMusic(true);
        musicIconOn.SetActive(true);
        musicIconOff.SetActive(false);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
}