using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasController : BaseSingleton<PlayerCanvasController>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;
    private int currentCoin = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        var maxHealth = GameDataManager.Instance.PlayerMaxHP;
        currentCoin = GameDataManager.Instance.Coin;
        UpdateHealthBarDisplay(maxHealth, maxHealth);
        UpdateCoinDisplay(currentCoin);
        Stamina.Instance.AddStamina(3);
    }
    public void UpdateHealthBarDisplay(float currentHealth, float maxHealth) {
        float healthPercent = currentHealth/maxHealth;
        healthSlider.value = healthPercent;
    }

    public void AddCoinDisplay(int coin)
    {
        currentCoin += coin;
        GameDataManager.Instance.Coin = currentCoin;
        UpdateCoinDisplay(currentCoin);
    }

    public void UpdateCoinDisplay(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void OnPauseClicked() {
        if (!pauseUI.activeInHierarchy) {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void GameOverUI () {
        if (!gameOverUI.activeInHierarchy) {
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
        }
    }
}
