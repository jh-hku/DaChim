using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTRTunnel : MonoBehaviour
{

    TunnelSpawner tunnelSpawner;
    // Start is called before the first frame update
    void Start()
    {
        tunnelSpawner = GameObject.FindObjectOfType<TunnelSpawner>();
    }
    
    /*
    void OnBecameInvisible()
    {
        // Destroy the bullet
        tunnelSpawner.SpawnMTRTunnel(true);
        Destroy(gameObject);
    }*/

    
    private void OnTriggerExit(Collider other)
    {
        
        // Destroy gameObject 2 sec after player leave the trigger
        if (other.gameObject.tag == "Player")
        {
            tunnelSpawner.SpawnMTRTunnel(true);
            Destroy(gameObject, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
