using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerScript : MonoBehaviour
{


    //public GameObject focusObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtObject(Vector3 focusObject)
    { 
        transform.LookAt(focusObject);
    }
}
