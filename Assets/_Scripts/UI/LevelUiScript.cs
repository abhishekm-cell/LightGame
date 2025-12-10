using System.Collections.Generic;
using UnityEngine;

public class LevelUiScript : MonoBehaviour
{
    private UIManager uIManager;

    public void SetRefrence(UIManager uIManager)
    {
        this.uIManager = uIManager;
    }

    private List<LevelDataSave> levelDataList;

    
}