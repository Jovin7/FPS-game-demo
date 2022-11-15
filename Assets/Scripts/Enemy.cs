using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;
    public int scoreToGive;
    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;

    private List<Vector3> path;
    public float yPathOffset;
    private Weapon weapon;
    

    private GameObject target;

    private void Start()
    {
       
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<Player>().gameObject;
        InvokeRepeating("UpdatePath", 0f,0.5f);
    }
    private void Update()
    {
        float dis = Vector3.Distance(transform.position, target.transform.position);
        if (dis <= attackRange)
        {
            if (weapon.CanShoot())
                weapon.Shoot();
        }
        else
        {
            ChaseTarget(); 
        }
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.LookAt(target.transform);
        //float angle = Mathf.Atan2
    }
    void ChaseTarget()
    {
        if (path.Count == 0)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);
        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
        {
            path.RemoveAt(0);
        }
    }
    void UpdatePath()
    {
        //calc path
        NavMeshPath navmeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position,NavMesh.AllAreas,navmeshPath);
        
        // save as a list
        path = navmeshPath.corners.ToList<Vector3>();

    }
    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.instance.AddScore(scoreToGive);
        Destroy(gameObject);
    }
}
