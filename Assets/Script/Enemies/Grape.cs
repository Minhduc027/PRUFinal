using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float spreadAngle = 15f;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack() {
        myAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }
    private void SpawnFanShapeProjectiles(Vector3 targetPosition)
    {
        // Spawn 3 projectiles in a fan-shaped pattern
        for (int i = -1; i <= 1; i++)
        {
            float angle = i * spreadAngle * Mathf.Deg2Rad;
            Vector3 spawnPosition = transform.position;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            GameObject projectile = Instantiate(grapeProjectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
            StartCoroutine(DestroyProjectileAfterDuration(projectile, duration));
        }
    }

    private IEnumerator DestroyProjectileAfterDuration(GameObject projectile, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(projectile);
    }
    public void SpawnProjectileAnimEvent() {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
