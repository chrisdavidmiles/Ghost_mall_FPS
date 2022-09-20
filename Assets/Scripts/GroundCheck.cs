using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update


    public bool IsThisGrounded()
    {
        return isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
