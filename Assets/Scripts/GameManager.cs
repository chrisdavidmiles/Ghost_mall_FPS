using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxFrameRate = 60;
    public static GameManager instance;
    //public PauseScript pauseScript;

    // Start is called before the first frame update
    void Start()
    {
        //pauseScript = GameObject.FindObjectOfType<PauseScript>();
        DontDestroyOnLoad(gameObject);

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxFrameRate;
    }
}
