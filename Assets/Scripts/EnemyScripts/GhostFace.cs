using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFace : MonoBehaviour
{

    //Variables for fieldOfView
    public float viewRadius = 60;
    public float viewAngle = 45f;
    public bool canSeePlayer = false;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask wallsMask;



    public float health = 100;
    public GameObject player;
    public float enemySpeed = 10f;
    public float attackDistance = 5f;
    public Animator anim;
    private bool currentAttack = false;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(FOVRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer && !currentAttack)
        {
            MoveTowardsPoint(player.transform.position);
            transform.LookAt(player.transform);
 
            if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                Debug.Log("Smells like ATTACKS");
                StartCoroutine(Attack());
            }
        }
    }




    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfView();
        }

    }

    private void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, wallsMask))
                    {
                        canSeePlayer = true;
                    }
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }



    private void MoveTowardsPoint(Vector3 movingToPoint)
    {
        transform.position = transform.position + ((movingToPoint - transform.position).normalized * Time.deltaTime) * enemySpeed;
    }


    private IEnumerator Attack()
    {
        currentAttack = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attacking", false);
        currentAttack = false;
        transform.LookAt(player.transform);
    }


    public void TakeDamage(float damageTaken)
    {
        //Debug.Log("OUCH CHARLIE THAT HURTS");
        health -= damageTaken;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


}
