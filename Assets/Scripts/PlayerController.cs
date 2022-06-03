using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    //public Slider healthSlider;


    public float health = 100f;
    public float moveSpeed = 5f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -20f;
    public float jumpHeight = 10f;
    public float maxHealth;

    private bool canRegainHealth;
    public float healthRegenTimer;
    public float healthRegenSpeed = 15;


    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    public LayerMask enemyLayer;
    Vector3 velocity;
    public bool isGrounded;
    public Animator anim;
    public Camera playerCam;
    public Transform playerTransform;



    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MovementSystem();
        HealthSystem();

        //Press jump to jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        //Running
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            moveSpeed = runSpeed;
        }
        else
            moveSpeed = walkSpeed;

    }





    public void MovementSystem()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity.y = jumpHeight;
    }



    public void HealthSystem()
    {
        //healthSlider.value = health;

        if (health < maxHealth)
            health = health + (healthRegenSpeed * Time.deltaTime);


        if (!canRegainHealth)
            StartCoroutine(WaitForHealthRegen(healthRegenTimer));

    }




    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //Currently commented out, but will be enabled when pausing is enabled
        //pauseScript.GameOver();
    }

    IEnumerator WaitForHealthRegen(float waitBeforeHealthRegens)
    {
        yield return new WaitForSeconds(waitBeforeHealthRegens);
        canRegainHealth = true;
    }

    IEnumerator CatchingBreath(float tiredTimer)
    {
        yield return new WaitForSeconds(tiredTimer);
    }

    public void IvySettings()
    {
        moveSpeed = 20f;
        walkSpeed = 20f;
        runSpeed = 40f;
        gravity = -22;
        jumpHeight = 12f;
        maxHealth = 200;
        health = maxHealth;
        healthRegenTimer = 0.25f;
    }


}
