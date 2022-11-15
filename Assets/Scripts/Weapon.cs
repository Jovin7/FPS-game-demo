using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool;
    public Transform muzzle;
    public float bulletSpeed;

    public int curAmmo;
    public int maxAmmo;

    public bool infiniteAmmo;

    public float shootRate;
    private float lastShootTime;
    private bool isPlayer;
    public AudioClip shootSFX;
    private AudioSource audioSource;
    private void Awake()
    {
        if (GetComponent<Player>())
            isPlayer = true;

        audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot()
    {
        if (Time.time - lastShootTime >= shootRate)
        { 
            if (curAmmo > 0 || infiniteAmmo)
                return true;                
        }
        return false;
    }
    public void Shoot()
    {
        audioSource.PlayOneShot(shootSFX);
        lastShootTime = Time.time;
        curAmmo--;
        if (isPlayer) GameUI.instance.UpdateAmmoText(curAmmo, maxAmmo);
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        //GameObject bullet = Instantiate(bulletPrefab, muzzle.position,muzzle.rotation);
        
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }
   
}
