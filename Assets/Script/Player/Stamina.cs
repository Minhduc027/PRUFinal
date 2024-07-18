using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : BaseSingleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected void Awake() {
        base.Awake();
        
        maxStamina = startingStamina;
        CurrentStamina = GameDataManager.Instance.CurrentStamina;
    }

    private void Start() {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina() {
        RemoveStamina(1);
        UpdateStaminaImages();
    }

    public void RefreshStamina() {
        if (CurrentStamina < maxStamina) {
            AddStamina(1);
        }
        UpdateStaminaImages();
    }

    /*private IEnumerator RefreshStaminaRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }*/

    private void UpdateStaminaImages() {

        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1) {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            } else {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }
    }

    public void AddStamina(int stamina) {
        CurrentStamina += stamina;
        GameDataManager.Instance.CurrentStamina = CurrentStamina;
    }

    private void RemoveStamina(int stamina) {
        CurrentStamina -= stamina;
        GameDataManager.Instance.CurrentStamina = CurrentStamina;
    }
}
