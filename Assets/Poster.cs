using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    public Texture[] Images;
    Plane poster;
    // Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        int ind = Random.Range(0, Images.Length);
        Material material = new Material(Shader.Find("Diffuse"));
        material.mainTexture = Images[ind];
        
        // poster = GameObject.FindObjectOfType<Plane>();
        Renderer rd = GetComponent<Renderer>();
        rd.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void addPoster()
    {


        // float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        //float x1 = transform.position.x - s;
        //float x2 = transform.position.x + s;
        // int x1 = -1;
        // int x2 = 2;
        // // 
        // int count = 0;

        // // Randomly pick a point within the spawn object 
        // for (int i = -1; i < 2; i++) {
        //     float prob = Random.Range(0f, 1f);

        //     if (prob > 0.5 & count < 2) {
        //         Vector3 spawnPoint = new Vector3(5 * i, -1.5f, transform.position.z);

        //         // Create an enemy at the 'spawnPoint' position  
        //         Instantiate(poster[Random.Range(0, poster.Count)], spawnPoint, Quaternion.identity);
        //         count++;
        //     }
            
        // }

        // // spawn one enemy if no enemy spawns
        // if (count == 0) {
        //     Vector3 spawnPoint = new Vector3(5 * Random.Range(x1, x2), -4.5f, transform.position.z);

        //     // Create an enemy at the 'spawnPoint' position  
        //     Instantiate(poster[Random.Range(0, poster.Count)], spawnPoint, Quaternion.identity);

        // }
        

    }
}
