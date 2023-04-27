/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    [SerializeField] CoinBar coinBar;
    //HyperCasual.Runner.GameManager gameMan;
    

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion
    HyperCasual.Runner.PlayerController playerController;

    public int coin_collected = 0;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public AudioSource audio;

    
    public void Heal(float health)
    {
        this.health += health;
        Debug.Log(health);
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        //Debug.Log(health);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    #region Coin
    public void IncrementCoin()
    {
        audio.Play();
        coin_collected++;
        coinBar.CoinAdding(1f);
        if (coin_collected == 100)
        {
            coin_collected = 0;
            //playerController.BeingCured();
            Heal(1.0f);
        }
        //Debug.Log(coin_collected);
    }
    #endregion
}
