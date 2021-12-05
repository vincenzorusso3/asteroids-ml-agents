using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class ShootDemoAgent : Agent
{
    public Rigidbody projectile;
    public float speed = 20;
    public float timeBetweenShots = 0.5f;  //2 spari al secondo
    private float timestamp;
    public AudioSource laser;
    public float RotateSpeed = 150f;
    private bool ShotAvaliable = true;
    private bool isAiming=false;
    private int StepsUntilShotIsAvaliable = 0;
    public int minStepsBetweenShots = 50;
    private RaycastHit hit;
    private RayPerceptionInput rayPerception;
    private Rigidbody instantiatedProjectile;
  
    
    public int Reach=200;

    private void Start()
    {
        laser = GetComponent<AudioSource>();
     
        
    }

    private void FixedUpdate()
    {
        if (!ShotAvaliable)
        {
            StepsUntilShotIsAvaliable--;

            if (StepsUntilShotIsAvaliable <= 0)
                ShotAvaliable = true;
        }

        AddReward(-1f / MaxStep);


        

        if (Physics.Raycast (transform.position, transform.TransformDirection(new Vector3(0, 0, speed)), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "big" ||
                hit.transform.tag == "mid" ||
                hit.transform.tag == "small")
            {
                //miro un asteroide
                isAiming = true;
            }
            else {
                isAiming = false;
            }
        }







        }

    public override void CollectObservations(VectorSensor sensor)
    {

        sensor.AddObservation(ShotAvaliable);
        sensor.AddObservation(isAiming);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        var continueActions = actions.ContinuousActions;
        if (Mathf.RoundToInt(continueActions[0]) >= 1 && isAiming)
        {
            shoot();
        }

        transform.Rotate(0.0f, continueActions[1] * speed, 0.0f);

    }

    public override void OnEpisodeBegin()
    {
        
        ShotAvaliable = true;
        isAiming=false;

    }





    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continueActions = actionsOut.ContinuousActions;


        continueActions[0] = Time.time >= timestamp && Input.GetKey(KeyCode.Space) ? 1f : 0f;

        continueActions[1] = Input.GetAxis("Horizontal");




    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindWithTag("big") == null &&
             GameObject.FindWithTag("mid") == null &&
             GameObject.FindWithTag("small") == null)
        {
            //level complete
            EndEpisode();
            SceneManager.LoadScene("Gioco");
        }
      
    }



    private void moveRight()
    {
        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }

    private void moveLeft()
    {
        transform.Rotate(Vector3.down * RotateSpeed * Time.deltaTime);
    }



    private void shoot()
    {
        if (!ShotAvaliable)
            { return; }
        laser.Play();
        instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        instantiatedProjectile.tag = "bullet";
        //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(speed, 0, 0)), Color.blue, 1f);

       
        Destroy(instantiatedProjectile.gameObject, 3f);
        timestamp = Time.time + timeBetweenShots;



        ShotAvaliable = false;
        StepsUntilShotIsAvaliable = minStepsBetweenShots;
    }

 

    public void muroHitPunish()
    {
        AddReward(-0.033f);
    }

    public void bigAsteroHitReward()
    {
        AddReward(1f);
    }

    public void midAsteroHitReward()
    {
        AddReward(2f);
    }

    public void smallAsteroHitReward()
    {
        AddReward(3f);
    }

    public void asteroidHitPlayer()
    {
        AddReward(-0.150f);
        EndEpisode();
        SceneManager.LoadScene("Gioco");
    }
}