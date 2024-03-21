using UnityEngine;

public class TurretCanonShootPlayer : MonoBehaviour
{
    public GameObject playerTarget;

    void Start()
    {
    }

    void Update()
    {
        RaycastHit hit;
        GameObject turretBody = transform.parent.gameObject;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.green);
        }
        transform.RotateAround(turretBody.transform.position, Vector3.up, 20 * Time.deltaTime);
        // transform.LookAt(playerTarget.transform, Vector3.left);
    }
}
