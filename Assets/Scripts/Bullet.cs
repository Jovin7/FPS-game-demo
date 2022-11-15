using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifeTime;
    private float shootTime;
    public GameObject hitParticle;
    private void OnEnable()
    {
        shootTime = Time.time;
    }
    private void Update()
    {
        if(Time.time-shootTime >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        GameObject obj = Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(obj, .5f);
        gameObject.SetActive(false);
    }
}
