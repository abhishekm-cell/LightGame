using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCorrect : MonoBehaviour
{
    [SerializeField] private GameObject childObj;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        OrientCorrect();
    }
    private void OrientCorrect()
    {
        childObj.transform.rotation = Quaternion.identity;
        Debug.Log("corrected" + childObj.transform.rotation);
    }
}
