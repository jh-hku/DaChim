using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int speed = 20;
    [SerializeField] Rigidbody rb;

    HyperCasual.Runner.PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<HyperCasual.Runner.PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            playerController.BeingCured();
            PlayerStats.Instance.Heal(1.0f);
            Debug.Log("Picked Food");
        }
        else if (collision.gameObject.tag == "HighObstacle")
        {
            Destroy(gameObject);
        }
    }


    // Gets called when the object goes out of the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    
    private void Update() {
        transform.Rotate(0, 180f * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        //Vector3 velocity = new Vector3(0, 0, -speed);
        //rb.velocity.z = -speed * Time.fixedDeltaTime * 50 ;
        Vector3 velocity = rb.velocity;
        velocity.z = -speed;
        //rb.velocity = velocity;
    }
    

}
