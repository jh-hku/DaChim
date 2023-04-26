using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{   
    // player object
    [SerializeField] Transform player;
    // distance between EmenySpawner and the player
    Vector3 offset;

   
    public float distPerFood = 1500f;
    private float distanceMoved = 0f;
    private Vector3 spawnPosition;
    
    public List<GameObject> food;

    // Variable to know how fast we should create new enemies
    public float spawnTime = 0.1f;
    int randNum;

    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - player.position;
        spawnPosition = transform.position;
        
    }

    // Update is called once per frame
    private void Update()
    {
        // separated the EmenySpawner and the player with fixed distance
        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;


        distanceMoved += Vector3.Distance(transform.position, spawnPosition);
        spawnPosition = transform.position;

        if (distanceMoved >= distPerFood)
        {
            addFood();
            distanceMoved = 0f;
        }


    }

    
    void addFood()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        //float x1 = transform.position.x - s;
        //float x2 = transform.position.x + s;
        int x1 = -1;
        int x2 = 2;
        // 
        int count = 0;

        // Randomly pick a point within the spawn object 
        for (int i = -1; i < 2; i++) {
            float prob = Random.Range(0f, 1f);

            if (prob > 0.5 & count < 2) {
                Vector3 spawnPoint = new Vector3(5 * i, -4.5f, transform.position.z);

             
                Instantiate(food[Random.Range(0, 5)], spawnPoint, Quaternion.identity);
                count++;
            }
            
        }

        
        if (count == 0) {
            Vector3 spawnPoint = new Vector3(5 * Random.Range(x1, x2), -4.5f, transform.position.z);

           
            Instantiate(food[Random.Range(0, 5)], spawnPoint, Quaternion.identity);

        }
        

    }
}
