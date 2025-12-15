using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUIController : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button nextLevelBtn;

    [Header("starts")]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private UIManager uIManager;


    public void SetRefrence(UIManager uIManager)
    {   
        this.uIManager = uIManager;
    }

    private void Awake()
    {
        restartBtn.onClick.AddListener(() =>
        {
            uIManager.RestartLevel();
            gameObject.SetActive(false);
        });
        homeBtn.onClick.AddListener(() =>
        {
            uIManager.ActivateMainMenu();   
        });
        nextLevelBtn.onClick.AddListener(() =>
        {
            uIManager.LoadNextLevel();
            gameObject.SetActive(false);
        });
    }

    public void SetStart(int starts)
    {
        star1.SetActive(starts >= 1);
        star2.SetActive(starts >= 2);
        star3.SetActive(starts >= 3);
    }
}