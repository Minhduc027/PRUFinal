using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Knockback), typeof(Flash))]
public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoverTime = 1f;

    [SerializeField] private int currentHealth;
    private bool canTakeDamage;
    [SerializeField] private Knockback knockback;
    [SerializeField] private Flash flash;

    public bool isDead {  get; private set; }
    public delegate void OnPlayerDeath();
    //public static event OnPlayerDeath onPlayerDeath;

    protected override void Awake()
    {
        base.Awake();
        canTakeDamage = true;
        maxHealth =  GameDataManager.Instance.PlayerMaxHP;
        currentHealth = maxHealth;
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        
        EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
        if (enemyAI) {
            TakeDamage(1, other.transform);
        }
    }
    public void HealPlayer()
    {
        Healing(1);
    }

    public void TakeDamage (int damageAmount, Transform hitTransform) {
        if(!canTakeDamage) { return; }
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        LostHealth(damageAmount);
        StartCoroutine(DamageRecoveryRoutine());
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
        CheckPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine () {
        yield return new WaitForSeconds(damageRecoverTime);
        canTakeDamage = true;
    }

    private void Healing (int healingAmount) {
        AddHealth(healingAmount);
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
    }

    private void CheckPlayerDeath(){
        if(currentHealth <= 0 && !isDead) {
            Dead();
        }
    }
    readonly int DEATH_HASH = Animator.StringToHash("Death");
    public void Dead()
    {
        isDead = true;
        Destroy(ActiveWeapon.Instance.gameObject);
        ResetHealth();
        GetComponent<Animator>().SetTrigger(DEATH_HASH);
        StartCoroutine(DeadLoadRoutine());
    }
    private IEnumerator DeadLoadRoutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        GameDataManager.Instance.SetDefaultValue();
        PlayerCanvasController.Instance.GameOverUI();
    }

    public void AddHealth(int health) {
        if (currentHealth < maxHealth) {
            currentHealth += health;
            GameDataManager.Instance.CurrentPlayerHP = currentHealth;
        }
    }

    public void LostHealth(int health) {
        if (currentHealth - health >=0) {
            currentHealth -= health;
            GameDataManager.Instance.CurrentPlayerHP = currentHealth;
        }
    }

    public void ResetHealth() {
        this.currentHealth = 0;
    }

}
