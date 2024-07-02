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
            turretBehavior.OutlineTurret();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
            turretBehavior.RemoveOutline();
        }
    }

    void Start()
    {
        turretBehavior = GetComponentInParent<TurretBehavior>();
    }

    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.R))
        {
            turretBehavior.SwitchToTeam();
        }
    }
}
