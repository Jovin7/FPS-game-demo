using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Ammo,
    Health
}
public class Pickup : MonoBehaviour
{
    public PickupType pickupType;
    public int value;

    [Header("Bobbing")]
    public float rotateSpeed;
    public float bobbSpeed;
    public float bobHeight;
    private Vector3 startPos;
    private bool bobUp;

    [Header("Audio")]
    public AudioClip pickSFX;
    
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime);

        Vector3 offset = bobUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0);
        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobbSpeed * Time.deltaTime);
        if (transform.position == startPos + offset) bobUp = !bobUp;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Player player = other.GetComponent<Player>();
            switch (pickupType)
            {
                case PickupType.Health:
                    player.GiveHealth(value);
                    break;

                case PickupType.Ammo:
                    player.GiveAmmo(value);
                    break;
            }
            player.GetComponent<AudioSource>().PlayOneShot(pickSFX);
            Destroy(gameObject);
                
        }
           
    }

}
