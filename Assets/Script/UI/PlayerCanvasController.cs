using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasController : Singleton<PlayerCanvasController>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI coinText;
    private int currentCoin = 0;
    // Start is called before the first frame update
    public void UpdateHealthBarDisplay(float currentHealth, float maxHealth) {
        float healthPercent = currentHealth/maxHealth;
        healthSlider.value = healthPercent;
    }

    public void AddCoinDisplay(int coin)
    {
        currentCoin += coin;
        UpdateCoinDisplay(currentCoin);
    }

    public void UpdateCoinDisplay(int coin)
    {
        coinText.text = coin.ToString();
    }
}
