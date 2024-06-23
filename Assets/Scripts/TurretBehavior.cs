using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public bool isNeutral = true;
    TurretCanonShootPlayer cannonController;
    public Material blueTeam;

    void Start()
    {
        cannonController = GetComponentInChildren<TurretCanonShootPlayer>();
    }

    void Update()
    {
        cannonController.enabled = !isNeutral;        
    }

    public void SwitchToTeam()
    {
        isNeutral = false;
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; meshRenderers[i]; i += 1) {
            meshRenderers[i].material = blueTeam;
        }
    }
}
