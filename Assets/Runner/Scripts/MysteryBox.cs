using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBox : MonoBehaviour
{
    public int speed = 20;
    [SerializeField] Rigidbody rb;

    HyperCasual.Runner.PlayerController playerController;
    public Animator animator;
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController crash;
    public AudioSource audio;
    // public Text surpriseText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<HyperCasual.Runner.PlayerController>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = idle;
        // surpriseText.text = "";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.runtimeAnimatorController = crash;
            audio.Play();
            switch (Random.Range(0, 4))
            {
                case 0:
                    PlayerStats.Instance.Heal(0.5f);
                    PlayerStats.Instance.ShowMysteryResult("+ 0.5 ♥");
                    // surpriseText.text = "+ 0.5 ♥";
                    break;
                case 1:
                    PlayerStats.Instance.TakeDamage(0.5f);
                    PlayerStats.Instance.ShowMysteryResult("- 0.5 ♥");
                    // surpriseText.text = "- 0.5 ♥";
                    break;
                case 2:
                    PlayerStats.Instance.bonusCoin(5);
                    PlayerStats.Instance.ShowMysteryResult("+ 5 $$");
                    // surpriseText.text = "+ 5 $$";
                    break;
                case 3:
                    PlayerStats.Instance.deductCoin(5);
                    PlayerStats.Instance.ShowMysteryResult("- 5 $$");
                    // surpriseText.text = "- 5 $$";
                    break;
                default:
                    PlayerStats.Instance.bonusCoin(1);
                    PlayerStats.Instance.ShowMysteryResult("+ 1 $$");
                    // surpriseText.text = "+ 1 $$";
                    break;
            }
            // PlayerStats.Instance.Heal(0.5f);
            // Debug.Log("Box");
        }
        else if (collision.gameObject.tag == "HighObstacle")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    private void Update() {
        transform.Rotate(0, 90f * Time.deltaTime,0);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.z = -speed;
    }

}
