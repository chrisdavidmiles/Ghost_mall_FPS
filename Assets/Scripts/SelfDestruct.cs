using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float selfDestructTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestructStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SelfDestructStart()
    {
        float timer = 0;



        while (timer < selfDestructTimer)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        Destroy(this.gameObject);

    }
    
}
