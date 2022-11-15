using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    

    [Header("Camera")]
    public float lookSensitivity;
    public float maxLookX ;
    public float minLookX ;
    private float currotX;

    private Camera cam;
    private Rigidbody rg;
    private Weapon weapon;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        rg = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
    }
    private void Start()
    {
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
        GameUI.instance.UpdateAmmoText(weapon.curAmmo, weapon.maxAmmo);
        GameUI.instance.UpdateScoreText(0);
    }
    private void Update()
    {
        if (GameManager.instance.gamePaused) return;
        Move();
        CamLook();
        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();
        if (Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot())
                weapon.Shoot();
        }
           
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        rg.velocity = new Vector3(x, rg.velocity.y, z);
        Vector3 dir = transform.right *x + transform.forward *z;
        dir.y = rg.velocity.y;
        rg.velocity = dir;

    }
    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        
        if(Physics.Raycast(ray,1.1f))
            rg.AddForce(transform.up* jumpForce, ForceMode.Impulse);
    }
    void CamLook()
    {
        float y = Input.GetAxis("Mouse X")* lookSensitivity;
        currotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        currotX = Mathf.Clamp(currotX, minLookX, maxLookX);
        
        cam.transform.localRotation = Quaternion.Euler(-currotX, 0, 0);
        transform.localEulerAngles += Vector3.up * y;
    }
    public void TakeDamage(int damage)
    {
        curHp -= damage;
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
        if(curHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.instance.LoseGame();
    }
    public void GiveHealth(int health)
    {

        curHp = Mathf.Clamp(curHp + health, 0, maxHp);
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    }
    public void GiveAmmo(int ammo)
    {

        weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + ammo, 0, weapon.maxAmmo);
        GameUI.instance.UpdateAmmoText(weapon.curAmmo, weapon.maxAmmo);
    }
}
