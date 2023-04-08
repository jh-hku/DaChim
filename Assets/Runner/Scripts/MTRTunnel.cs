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

    private void OnTriggerExit(Collider other)
    {
        tunnelSpawner.SpawnMTRTunnel(true);
        // Destroy gameObject 2 sec after player leave the trigger 
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
