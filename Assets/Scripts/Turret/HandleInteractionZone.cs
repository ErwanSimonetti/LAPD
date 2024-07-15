using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInteractionZone : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    TurretBehavior turretBehavior;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
            if (turretBehavior.isNeutral)
                turretBehavior.OutlineTurret();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
            if (turretBehavior.isNeutral)
                turretBehavior.RemoveOutline();
        }
    }

    void Start()
    {
        turretBehavior = GetComponentInParent<TurretBehavior>();
    }

    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.R)) // use Input universal
        {
            turretBehavior.SwitchToTeam();
        }
    }
}
