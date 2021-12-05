using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroideColpito : MonoBehaviour
{
    public GameObject prefab;
    private Rigidbody parentRb;
    private Rigidbody rb;
    private GameObject newAst;
    public AudioSource audioSource;
    public AudioClip audioclip;
    private float midScale = 2.25f;
    private float smallScale = 1.5f;
    public GameObject player;
    public float timer;
    private bool visible;


    void restart() {
       // SceneManager.LoadScene("Gioco");
    }
    // Start is called before the first frame update
    void Start()
    {
       parentRb = GetComponent<Rigidbody>();

       
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    void  OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(audioclip);

            if (gameObject.tag == "mid" )
            {
                player.GetComponent<ShootDemoAgent>().midAsteroHitReward();
                //rimpicciolisco
                gameObject.tag = "small";
                gameObject.transform.localScale = new Vector3(smallScale,smallScale,smallScale);
                //istanzio nuovo con velocità del padre deviata leggermente a destra
                newAst = Instantiate(gameObject, transform.position, Quaternion.identity);
                newAst.name = gameObject.name;
                rb = newAst.GetComponent<Rigidbody>();
                rb.velocity = parentRb.velocity + new Vector3(1, 1, 0);

                newAst = Instantiate(gameObject, transform.position, Quaternion.identity);
                newAst.name = gameObject.name;
                rb = newAst.GetComponent<Rigidbody>();
                rb.velocity = parentRb.velocity + new Vector3(-1, -1, 0);

                if (gameObject.tag == "mid") {
                    Destroy(gameObject);
                }

            }

            if (gameObject.tag == "small") {
                player.GetComponent<ShootDemoAgent>().smallAsteroHitReward();
                Destroy(gameObject);
            }

            if (gameObject.tag == "big")
            {
                player.GetComponent<ShootDemoAgent>().bigAsteroHitReward();
                Destroy(other.gameObject);
                //rimpicciolisco
                gameObject.tag = "mid";
                gameObject.transform.localScale = new Vector3(midScale,midScale, midScale);
                //istanzio nuovo con velocità del padre deviata leggermente a destra
                newAst = Instantiate(gameObject, transform.position, Quaternion.identity);
                newAst.name = gameObject.name;
                rb = newAst.GetComponent<Rigidbody>();
                rb.velocity = parentRb.velocity + new Vector3(1, 1, 0);

            }

        }

         
     
       

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            player.GetComponent<ShootDemoAgent>().asteroidHitPlayer();
        }


    }


 


}