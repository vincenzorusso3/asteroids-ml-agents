using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroColpitoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "bullet")
        {
            //Debug.Log("Muro colpito");
            player.GetComponent<ShootDemoAgent>().muroHitPunish();
            Destroy(other.gameObject);
        }
    }
}
