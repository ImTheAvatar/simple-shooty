using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("hit "+other.name);

        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("hit " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
