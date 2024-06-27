using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefabs;
    [SerializeField] private Transform arrowSpawnPoint;
    readonly int FIRE_HASH = Animator.StringToHash("fire");
    private Animator myAnimator;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack()
    {
        Debug.Log("Bow Attack");
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefabs, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
