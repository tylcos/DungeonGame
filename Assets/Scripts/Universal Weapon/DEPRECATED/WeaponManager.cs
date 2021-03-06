﻿using System.Collections;
using System.Linq;
using UnityEngine;


[System.Obsolete("Use a WeaponController instead.", true)]
public class WeaponManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public Transform bulletSpawnPoint;

    public WeaponInfo[] WeaponInfos;



    [HideInInspector]
    public Weapon Weapon
    {
        get { return Weapons[CurrentWeapon]; }
    }
    [HideInInspector]
    public Weapon[] Weapons;
    [HideInInspector]
    public int CurrentWeapon = 0;



    private float weaponPos = 0f;
    private bool reloading = false;
    private int weaponBeingReloaded;

    public delegate void WeaponUpdateHandler();
    public event WeaponUpdateHandler WeaponDataChanged;
    private DungeonGameManager master;

    public int CurrentAmmo
    {
        get { return Weapon.CurrentAmmo;  }
    }

    public int MaxAmmo
    {
        get { return Weapon.Info.clipSize;  }
    }
    
    private void OnEnable()
    {
        if (WeaponInfos.Length == 0)
            throw new System.ArgumentOutOfRangeException("No weapon infos selected.");

        Weapons = WeaponInfos.Select(w => new Weapon(w)).ToArray();
        CurrentWeapon = 0;
    }



    void Update()
    {
        if (DungeonGameManager.InputMethod == DungeonGameManager.InputMethodType.Keyboard)
        {
            weaponPos += Mathf.Abs(Input.GetAxis("ScrollWheel"));

            if (Input.GetKeyDown(KeyCode.Tab))
                ++weaponPos;
        }
        else if (DungeonGameManager.InputMethod == DungeonGameManager.InputMethodType.Arcade)
        {
            if (Input.GetKeyDown(KeyCode.Minus))
                ++weaponPos;
        }

        
        weaponPos %= Weapons.Length;
        int flooredWeaponPos = Mathf.FloorToInt(weaponPos); 
        if (CurrentWeapon != flooredWeaponPos)
            ChangeWeapon(flooredWeaponPos);


        if(DungeonGameManager.InputMethod == DungeonGameManager.InputMethodType.Keyboard)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Weapon.CurrentAmmo = 0;
                Reload();
            }
        }

        WeaponDataChanged();
    }

    private void ChangeWeapon(int flooredWeaponPos)
    {
        CurrentWeapon = flooredWeaponPos;

        sr.sprite = Weapon.Info.weaponSprite;
        bulletSpawnPoint.localPosition = Weapon.Info.bulletSpawnOffset;
        reloading = false;



        if (Weapon.CurrentAmmo == 0)
            Reload();
    }


    public void Reload()
    {
        if (!reloading)
        {
            weaponBeingReloaded = CurrentWeapon;
            StartCoroutine(ReloadInternal());
        }

        reloading = true;
    }

    private IEnumerator ReloadInternal()
    {
        yield return new WaitForSeconds(Weapon.Info.reloadTime);

        if (CurrentWeapon == weaponBeingReloaded)
        {
            Weapon.CurrentAmmo = Weapon.Info.clipSize;
            reloading = false;
        }
    }
}