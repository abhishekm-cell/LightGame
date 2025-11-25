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
    private GameObject lightSource;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(drawManager.TouchCheck())
        {
            gameStarted = true;
            SpawnLightSource();
        }
        
    }


    void SpawnLightSource()
    {
        lightSource = Instantiate(lightSourcePrefab,startPointPrefab.transform.position, Quaternion.identity);   
    }


}
