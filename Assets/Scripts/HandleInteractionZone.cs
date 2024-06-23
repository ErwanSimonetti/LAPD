using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInteractionZone : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.R))
        {
            GetComponentInParent<TurretBehavior>().SwitchToTeam();
        }
    }
}
