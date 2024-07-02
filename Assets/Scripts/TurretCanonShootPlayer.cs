using UnityEngine;

public class TurretCanonShootPlayer : MonoBehaviour
{
    public GameObject playerTarget;
    public float rotationSpeed = 20;

    void Start()
    {
    }

    void Update()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        RaycastHit hit;
        GameObject turretBody = transform.parent.gameObject;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.green);
        }

        Vector3 forward = transform.TransformDirection(Vector3.up);
        Vector3 toOther = playerTarget.transform.position - transform.position;

        float rotation;
        float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.right), toOther);
        if (dotProduct < 0.02 && dotProduct > -0.02) {
            ShootProjectile();
            return;
        }
        else if (dotProduct > 0)
            rotation = rotationSpeed;
        else
            rotation = -rotationSpeed;

        transform.RotateAround(turretBody.transform.position, Vector3.up, rotation * Time.deltaTime);
    }

    private void ShootProjectile()
    {

    }
}
