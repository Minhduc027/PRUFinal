using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Knockback), typeof(Flash))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoverTime = 1f;

    [SerializeField] private int currentHealth;
    private bool canTakeDamage;
    [SerializeField] private Knockback knockback;
    [SerializeField] private Flash flash;

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;

    void Start()
    {
        canTakeDamage = true;
        currentHealth = maxHealth;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        if (enemyAI && canTakeDamage) {
            TakeDamage(1);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private void TakeDamage (int damageAmount) {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
        CheckPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine () {
        yield return new WaitForSeconds(damageRecoverTime);
        canTakeDamage = true;
    }

    private void Healing (int healingAmount) {
        currentHealth +=  healingAmount;
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
    }

    private void CheckPlayerDeath(){
        if(currentHealth <= 0 && onPlayerDeath != null) {
            onPlayerDeath();
        }
    }
}
