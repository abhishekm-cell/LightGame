using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // spawn the light source at the start PT of the game
    // spawn the end point of the game
    // spawn the obstacles
    // manage movement after touch has been made

    [SerializeField] private GameObject lightSourcePrefab;
    [SerializeField] private GameObject endPointPrefab;
    [SerializeField] private GameObject startPointPrefab;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BlackHole blackhole;
  

    void Start()
    {
        if(drawManager.TouchCheck())
        {
            gameStarted = true;
            Debug.Log("instantiate light source");
            //SpawnLightSource();
        }
    }

    public void SpawnLightSource()
    {
        
        GameObject newLightSource = Instantiate(lightSourcePrefab, startPointPrefab.transform.position, Quaternion.identity);
        
        
        newLightSource.GetComponent<LightSource>().SetGameManager(gameManager);
        
        
        gameManager.GetBlackHole().UpdateTarget(newLightSource);
    }
    

}
