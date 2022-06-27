using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HighlightMultipleButtons : MonoBehaviour
{

    //public Image mapButtonHighlight;
    public Image legendButtonHighlight;
    public Light mapLightHighlight;



    void Start()
    {
        //mapButtonHighlight.enabled = false;
        legendButtonHighlight.enabled = false;
        mapLightHighlight.enabled = false;
    }


    public void HighlightOption()
    {
        //mapButtonHighlight.enabled = true;
        legendButtonHighlight.enabled = true;
        mapLightHighlight.enabled = true;
    }


    public void RemoveHighlightOption()
    {
        //mapButtonHighlight.enabled = false;
        legendButtonHighlight.enabled = false;
        mapLightHighlight.enabled = false;
    }

}
