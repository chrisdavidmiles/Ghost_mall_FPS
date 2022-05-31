using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public Light moonlight;
    public bool bloodMoonEnabled;
    public float changeRate = 0.5f;
    public float changeTime = 1f;

    public bool shiftingPhase = false;



    // Start is called before the first frame update
    void Start()
    {
        if (moonlight.color == Color.red)
        {
            bloodMoonEnabled = true;
        }
        else
            bloodMoonEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !bloodMoonEnabled)
        {
            shiftingPhase = true;
            MoonShift(Color.red, changeRate);
            bloodMoonEnabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.F) && bloodMoonEnabled)
        {
            shiftingPhase = true;
            MoonShift(Color.gray, changeRate);
            bloodMoonEnabled = false;
        }
    }


    public void MoonShift(Color newColor, float changeRate)
    {
        moonlight.color = Color.Lerp(moonlight.color, newColor, 1f);
    }


}



