using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, 0);
    public Transform playerTransform;
    public Transform eyeTransform;


    private void Start()
    {
        transform.position = offset;
        transform.SetParent(playerTransform, true);

    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = offset;
        //playerTransform.parent.position = transform.position + offset;

        transform.rotation = Quaternion.Euler(eyeTransform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
}
