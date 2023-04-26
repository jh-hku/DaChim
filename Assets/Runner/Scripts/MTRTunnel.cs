using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTRTunnel : MonoBehaviour
{

    TunnelSpawner tunnelSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float leftRightPosition = 3.3f;
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

    public void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        int random_XPos = Random.Range(-1, 2);
        Vector3 point = new Vector3(
            random_XPos * leftRightPosition,
            -4f,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        //if (point != collider.ClosestPoint(point))
        //{
        //    point = GetRandomPointInCollider(collider);
        //}

        return point;
    }
}
