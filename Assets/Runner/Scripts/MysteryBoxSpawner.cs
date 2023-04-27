using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;

    public float distPerBox = 500f;
    private float distanceMoved = 0f;
    private Vector3 spawnPosition;
    public float landDistance = 3.75f;
    private float time = 0f; 
    
    public GameObject box;

    public float spawnTime = 3f;
    int randNum;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
        spawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;

        distanceMoved += Vector3.Distance(transform.position, spawnPosition);
        spawnPosition = transform.position;

        if (distanceMoved >= distPerBox)
        {
            addBox();
            distanceMoved = 0f;
        }


    }

    void addBox()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        int x1 = -1;
        int x2 = 2;
        int count = 0;

        // Randomly pick a point within the spawn object 
        for (int i = -1; i < 2; i++) {
            float prob = Random.Range(0f, 1f);

            if (prob > 0.5 & count < 2) {
                Vector3 spawnPoint = new Vector3(landDistance * i, -4.5f, transform.position.z);
             
                Instantiate(box, spawnPoint, Quaternion.identity);
                count++;
            }
            
        }

        if (count == 0) {
            Vector3 spawnPoint = new Vector3(5 * Random.Range(x1, x2), -4.5f, transform.position.z);
            Instantiate(box, spawnPoint, Quaternion.identity);
        }
    }

}


