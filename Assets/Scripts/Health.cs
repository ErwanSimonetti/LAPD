using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthPoints = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        if (healthPoints <= 0)
            BroadcastMessage("Die");
    }

    public void ApplyDamage(float damage)
    {
        healthPoints -= damage;
    }
}
