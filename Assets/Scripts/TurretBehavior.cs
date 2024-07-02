using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public bool isNeutral = true; // change name
    TurretCanonShootPlayer cannonController;
    public Material blueTeam;
    private Material outlineMaterial;
    private MeshRenderer[] meshRenderers;

    void Start()
    {
        cannonController = GetComponentInChildren<TurretCanonShootPlayer>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        outlineMaterial = new Material(Shader.Find("Custom/Outline"));
    }

    void Update()
    {
        cannonController.enabled = !isNeutral;
    }

    public void SwitchToTeam()
    {
        Material[] newMaterials = meshRenderers[1].materials;

        newMaterials[0] = blueTeam;
        meshRenderers[1].materials = newMaterials;
        meshRenderers[2].materials = newMaterials;
        isNeutral = false;
    }

    public void OutlineTurret()
    {
        Material[] newMaterials = new Material[meshRenderers[1].materials.Length + 1];
 
        for (int i = 0; i < meshRenderers[1].materials.Length; i++)
        {
            newMaterials[i] = meshRenderers[1].materials[i];
        }

        newMaterials[newMaterials.Length - 1] = outlineMaterial;
        meshRenderers[1].materials = newMaterials;
        meshRenderers[2].materials = newMaterials;
    }

    public void RemoveOutline()
    {
        Material[] currentMaterials = meshRenderers[1].materials;

        if (currentMaterials.Length > 1)
        {
            Material[] newMaterials = new Material[currentMaterials.Length - 1];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = currentMaterials[i];
            }

            meshRenderers[1].materials = newMaterials;
            meshRenderers[2].materials = newMaterials;
        }
    }
}
