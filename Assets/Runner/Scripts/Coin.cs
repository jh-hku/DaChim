using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("a");
        // Check That the object we collided with is the player
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        PlayerStats.Instance.IncrementCoin();
        //Debug.Log("a");
        //FindObjectOfType<AudioManager>().PlaySound("PickCoin");
        //Add to the player's score
        //GameManager.inst.IncrementCoin();

        //Destroy this coin object
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}