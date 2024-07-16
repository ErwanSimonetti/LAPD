using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : MonoBehaviour
{
    public int projectileSpeed = 10;
    public float projectileLifespan = 3f;
    private float timeOfShot = 0f;

    void Start()
    {
        timeOfShot = Time.time;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        if (Time.time - projectileLifespan > timeOfShot)
            Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(transform.parent.gameObject);
        if (other.gameObject.tag == "GameController")
            other.gameObject.BroadcastMessage("ApplyDamage", 10.0);
    }

    public int getProjectileSpeed()
    {
        return projectileSpeed;
    }
}
