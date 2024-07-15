using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLazer;
    [SerializeField] private Transform magicLazerSpawnPoint;

    private Animator myAnimator;
    private int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }


    public void Attack()
    {
        myAnimator.SetTrigger(AttackHash);
    }
    
    public void SpawnStaffProjectileEvent()
    {
        GameObject newLazer = Instantiate(magicLazer, magicLazerSpawnPoint.position, Quaternion.identity);
        newLazer.GetComponent<MagicLazer>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
