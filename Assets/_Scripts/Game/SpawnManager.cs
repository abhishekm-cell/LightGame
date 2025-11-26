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
        if(drawManager.TouchCheck())
        {
            gameStarted = true;
            Debug.Log("instantiate light source");
            //SpawnLightSource();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    // void SpawnLightSource()
    // {
    //     lightSource = Instantiate(lightSourcePrefab,startPointPrefab.transform.position, Quaternion.identity);   
    // }


}
