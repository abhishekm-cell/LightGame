using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private UIManager uIManager;
    [SerializeField] private LevelSelectPanelUIController levelSelectPanelUIController;

    public void SetRefrence(UIManager uIManager)
    {
        this.uIManager = uIManager;
        levelSelectPanelUIController.SetRefrence(uIManager);
    }
}