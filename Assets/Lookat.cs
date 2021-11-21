using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    public static GameObject lookAt;
    public GameObject LookAt;
    Vector3 direction;
    Quaternion targetRotation;
    public float lookSpeed=200f;
    GameObject closest;
    public float FightDistance=10;
    // Start is called before the first frame update
    void Start()
    {
        lookAt = LookAt;
    }
    
    GameObject findTarget()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        //Edit Enemy in the FindObjectsOfType to a component on the object you
        //want to find nearest 
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        return closestEnemy;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (lookAt != null)
        {
            direction = lookAt.transform.position - transform.position;
            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
        closest=findTarget();
        if (closest == null)
            return;
        if (Vector3.Distance(closest.transform.position, gameObject.transform.position)<=FightDistance)
        {
            if(lookAt==null)
            {
                lookAt=closest;
                return;
            }
            if(Vector3.Distance(lookAt.transform.position,gameObject.transform.position)>Vector3.Distance(closest.transform.position, gameObject.transform.position))
            {
                lookAt = closest;
            }
        }
        if(Vector3.Distance(closest.transform.position, gameObject.transform.position) > FightDistance && lookAt!=null)
        {
            lookAt = null;
        }
    }
}
