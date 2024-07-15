using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
