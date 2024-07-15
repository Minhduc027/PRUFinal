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
        currentHealth = maxHealth;
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
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
        }
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(currentHealth, maxHealth);
    }

    public void TakeDamage (int damageAmount, Transform hitTransform) {
        if(!canTakeDamage) { return; }
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
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
        if(currentHealth <= 0 && !isDead) {
            Dead();
        }
    }
    readonly int DEATH_HASH = Animator.StringToHash("Death");
    public void Dead()
    {
        isDead = true;
        Destroy(ActiveWeapon.Instance.gameObject);
        currentHealth = 0;
        GetComponent<Animator>().SetTrigger(DEATH_HASH);
        StartCoroutine(DeadLoadRoutine());
    }
    private IEnumerator DeadLoadRoutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        SceneManager.LoadScene("SampleScene");
        PlayerCanvasController.Instance.UpdateHealthBarDisplay(maxHealth, maxHealth);
        PlayerCanvasController.Instance.UpdateCoinDisplay(0);
    }

}
