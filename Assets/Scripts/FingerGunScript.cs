using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Rendering;


public class FingerGunScript : MonoBehaviour
{
    public int damage = 40;
    public int limbMultiplier = 1;
    public int headshotMultiplier = 2;
    public float rateOfFire = 0.25f;

    public float range;

    //Magazine stats
    public int magazineSize = 30;
    public int bulletsLoaded = 0;
    public float reloadSpeed = 1f;

    public int bulletsPerTriggerPull = 1;
    public bool readyForNextShot = true;



    //Other Components
    public Camera fpsCam;
    public Animator anim;


    public LineRenderer bulletLineRenderer;
    public float bulletTravelTime = 0.1f;
    public float laserOffset = 0.25f;
    public float distanceSpeed = 50f;
    public GameObject muzzle;
    public ParticleSystem muzzleFlash;
    public LayerMask shootableLayers;
    public GameObject bulletHole;




    // Start is called before the first frame update
    void Start()
    {

    }



    void Update()
    {

        if (Input.GetMouseButton(0) && readyForNextShot)
        {
            for (int i = 0; i < (bulletsPerTriggerPull); i++)
            {
                Shoot();

            }
        }

    }

    


    public void Shoot()
    {
        bulletLineRenderer.enabled = true;
        Vector3 directionRay = fpsCam.transform.TransformDirection(0, 0, 1);
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, directionRay, out hit, range, shootableLayers))
        {
            GhostFace target = hit.transform.GetComponentInParent<GhostFace>();
            Debug.Log(hit.point);

            if (target != null)
            {
                if (hit.collider.name == "Limbs")
                {
                    target.TakeDamage(damage * limbMultiplier);
                }
                else if (hit.collider.name == "Head")
                {
                    target.TakeDamage(damage * headshotMultiplier);
                }
                else
                    target.TakeDamage(damage);

                Debug.Log("We hit something");
            }

            /*if (hit.collider.tag == "Wall" || hit.collider.tag == "Ground")
            {
                //Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            }*/
            Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
        }







        if (hit.collider != null)
        {
           
            StartCoroutine(SpawnLineTrail(hit.point, Vector3.Distance(muzzle.transform.position, hit.point)));
            //muzzleFlash.Play();
            
        }
        else
        {
            StartCoroutine(SpawnLineTrail((muzzle.transform.position + (fpsCam.transform.forward + directionRay) * range), range));
            Debug.Log("We did not hit anything, but we're moving ahead");
        }


        
        muzzleFlash.Play();

        readyForNextShot = false;
        anim.SetBool("Firing", true);

        StartCoroutine(RateOfFire());
        
    }

    IEnumerator RateOfFire()
    {
        //anim.SetBool("Firing", false);
        yield return new WaitForSeconds(rateOfFire);
        readyForNextShot = true;
        
    }


    public IEnumerator SpawnLineTrail(Vector3 hitPoint, float distance)
    {
        float timer = 0;
        Vector3 startPosition = muzzle.transform.position;


        int currentPositions = bulletLineRenderer.positionCount;
        bulletLineRenderer.positionCount = currentPositions + 2;

        float timeBasedOnDistance = bulletTravelTime * ((Mathf.Abs(distance) / distanceSpeed));


        while (timer < timeBasedOnDistance)
        {
            yield return null;

            //Front end of laser
            //bulletLineRenderer.SetPosition(currentPositions, muzzle.transform.position);
            bulletLineRenderer.SetPosition(currentPositions, Vector3.Lerp(muzzle.transform.position, hitPoint, timer));
            

            //Back end of laser. Lags a little behind the front
            if (timer > laserOffset)
            {
                bulletLineRenderer.SetPosition(currentPositions + 1, Vector3.Lerp(muzzle.transform.position, hitPoint, (timer - laserOffset)));
                //Instantiate(bulletHole, hitPoint, Quaternion.LookRotation(hitPoint));
            }
            else
            {
                bulletLineRenderer.SetPosition(currentPositions + 1, muzzle.transform.position);
                //Instantiate(bulletHole, hitPoint, Quaternion.LookRotation(hitPoint));
            }


            timer += Time.deltaTime;
            
        }

        

        bulletLineRenderer.positionCount = 0;
        bulletLineRenderer.enabled = false;
        anim.SetBool("Firing", false);
    }


}
