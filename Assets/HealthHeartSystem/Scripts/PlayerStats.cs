/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

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
    
    public GameObject mysteryBox;
    public Text mysteryBoxText;
    public Timer timer;
    private float crashTime = -1;

    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            float currTime = timer.GetElapsedTime();
            if (currTime-crashTime > 1)
            {
                mysteryBox.SetActive(false);
            }
        }
    }
    
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

    public void ShowMysteryResult(string itemName)
    {
        crashTime = timer.GetElapsedTime();
        mysteryBox.SetActive(true);
        mysteryBoxText.text = itemName;
        if (itemName.Substring(0,1) == "+")
        {
            mysteryBoxText.color = Color.green;
        }
        else 
        {
            mysteryBoxText.color = Color.red;
        }
    }

    #region Coin
    public void IncrementCoin()
    {
        audio.Play();
        coin_collected++;
        coinBar.CoinAdding(1);
        if (coin_collected == 100)
        {
            coin_collected = 0;
            //playerController.BeingCured();
            Heal(1.0f);
        }
        //Debug.Log(coin_collected);
    }
    #endregion

    public void bonusCoin(int num)
    {
        audio.Play();
        coin_collected += num;
        coinBar.CoinAdding(num);
        if (coin_collected >= 100)
        {
            coin_collected = 0;
            //playerController.BeingCured();
            Heal(1.0f);
        }
        //Debug.Log(coin_collected);
    }

    public void deductCoin(int num)
    {
        audio.Play();
        coin_collected -= num;
        coinBar.CoinDeducting(num);
        if (coin_collected <= 0)
        {
            coin_collected = 0;
        }
    }
}
