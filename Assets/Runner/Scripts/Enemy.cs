using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 100;
    [SerializeField] Rigidbody agent;
    public Animator ani;
    HyperCasual.Runner.PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<HyperCasual.Runner.PlayerController>();
        Quaternion newRotation = Quaternion.Euler(0, 90, 0);
        gameObject.transform.rotation = newRotation;
        agent.velocity = new Vector3(0, 0, -speed);
        ani.SetInteger("arms", 1);
        ani.SetInteger("legs", 1);

        // agent = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Kiil the player
            playerController.ResetSpeed();
            playerController.GotHitted();
            PlayerStats.Instance.TakeDamage(1.0f);
            Destroy(gameObject);
        }
    }


    // Gets called when the object goes out of the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    
    private void FixedUpdate()
    {
        //Vector3 velocity = new Vector3(0, 0, -speed);
        //rb.velocity.z = -speed * Time.fixedDeltaTime * 50 ;
        //Vector3 velocity = rb.velocity;
        //velocity.z = -speed;

        //Vector3 velocity = agent.velocity;
        //velocity = new Vector3(0, 0, -speed);
        
        //agent.velocity = velocity;
    }


}
