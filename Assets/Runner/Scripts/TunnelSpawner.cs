using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpawner : MonoBehaviour
{

    [SerializeField] GameObject MTRTunnel;
    Vector3 nextSpawnPoint;
    // Start is called before the first frame update
    public void SpawnMTRTunnel(bool spawnItems)
    {
        // spawn MTRTunnel game object at nextSpawnPoint with no rotation
        GameObject temp = Instantiate(MTRTunnel, nextSpawnPoint, Quaternion.identity);
        // get nextSpawnPoint from new groundTile game object
        nextSpawnPoint = temp.transform.GetChild(5).transform.position;

        if (spawnItems)
        {
            //temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<MTRTunnel>().SpawnCoins();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnMTRTunnel(false);
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnMTRTunnel(true);
        }
    }
}
