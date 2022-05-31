using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailScript : MonoBehaviour
{
    public List<Vector3> points;
    LineRenderer line;

    public int maxSize = 15;


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>() {};
    }

    // Update is called once per frame
    void Update()
    {

    }
}
