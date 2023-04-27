//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinBar: MonoBehaviour
{
	public static CoinBar Instance;

	public Image currentCoinBar;
	public Image currentCoinGlobe;
	public Text Coin_Text;
	public float Coin = 0f;
	public float Max_Coin = 100f;

	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	public bool Regenerate = true;
	public float regen = 0.1f;
	public float regenUpdateInterval = 1f;

	public bool GodMode;

	//==============================================================
	// Awake
	//==============================================================
	void Awake()
	{
		Instance = this;
	}
	
	//==============================================================
	// Awake
	//==============================================================
  	void Start()
	{
		UpdateGraphics();
	}

	//==============================================================
	// Update
	//==============================================================
	void FixedUpdate ()
	{
		UpdateCoinBar();
		UpdateCoinGlobe();
    }

	//==============================================================
	// Regenerate Health & Mana
	//==============================================================
	//private void Regen()
	//{

	//	if (timeleft <= 0.0) // Interval ended - update health & mana and start new interval
	//	{
	//		// Debug mode
	//		if (GodMode)
	//		{
 //               CoinAdding(maxHitPoint);
	//			//RestoreMana(maxManaPoint);
	//		}
	//		else
	//		{
 //               CoinAdding(regen);
	//			//RestoreMana(regen);				
	//		}

	//		UpdateGraphics();

	//	}
	//}

	//==============================================================
	// Health Logic
	//==============================================================
	private void UpdateCoinBar()
	{
		float ratio = Coin / Max_Coin;
        currentCoinBar.rectTransform.localPosition = new Vector3(currentCoinBar.rectTransform.rect.width * ratio - currentCoinBar.rectTransform.rect.width, 0, 0);
        Coin_Text.text = Coin.ToString ("0") + "/" + Max_Coin.ToString("0");
	}

	private void UpdateCoinGlobe()
	{
		float ratio = Coin / Max_Coin;
        currentCoinGlobe.rectTransform.localPosition = new Vector3(0, currentCoinGlobe.rectTransform.rect.height * ratio - currentCoinGlobe.rectTransform.rect.height, 0);
        Coin_Text.text = Coin.ToString("0") + "/" + Max_Coin.ToString("0");
	}

	//public void TakeDamage(float Damage)
	//{
	//	hitPoint -= Damage;
	//	if (hitPoint < 1)
	//		hitPoint = 0;

	//	UpdateGraphics();

	//	StartCoroutine(PlayerHurts());
	//}

	public void CoinAdding(float coin)
	{
        Coin += coin;
        if (Coin > Max_Coin)
            Coin = 0;

		UpdateGraphics();
	}

	public void CoinDeducting(float coin)
	{
        Coin -= coin;
        if (Coin <= 0)
            Coin = 0;

		UpdateGraphics();
	}

	public void SetCoin(float max)
	{
		Max_Coin += (int)(Max_Coin * max / 100);

		UpdateGraphics();
	}



	//==============================================================
	// Update all Bars & Globes UI graphics
	//==============================================================
	private void UpdateGraphics()
	{
		UpdateCoinBar();
		UpdateCoinGlobe();

	}
}
