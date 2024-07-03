using UnityEngine;

public class TurretCanonShootPlayer : MonoBehaviour
{
    public GameObject playerTarget;
    public float rotationSpeed = 60;
    private GameObject turretBody;

    void Start()
    {
        turretBody = transform.parent.gameObject;
    }

    void Update()
    {
        AimAtPlayer();
    }

    private void DrawDebugShootingRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 10))
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
        else
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.green);
    }

    private float HandleHorizontalRotation()
    {
        Vector3 toOther = playerTarget.transform.position - transform.position;
        float rotation = 0;
        float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.right), toOther);

        if (dotProduct < 0.02 && dotProduct > -0.02) {
            ShootProjectile();
        }
        else if (dotProduct > 0)
            rotation = rotationSpeed;
        else
            rotation = -rotationSpeed;
        transform.RotateAround(turretBody.transform.position, Vector3.up, rotation * Time.deltaTime);
        return(dotProduct);
    }

    private float HandleVerticalRotation()
    {
        Vector3 toOther = playerTarget.transform.position - transform.position;
        float rotation = 0;
        float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.forward), toOther);

        if (dotProduct < 0.02 && dotProduct > -0.02) {
            ShootProjectile();
        }
        else if (dotProduct > 0)
            rotation = rotationSpeed;
        else
            rotation = -rotationSpeed;
        transform.Rotate(Vector3.right, rotation * Time.deltaTime);
        return (dotProduct);
    }

    private bool IsEnemyBehind()
    {
        Vector3 enemyDirectionLocal = transform.InverseTransformPoint(playerTarget.transform.position);

        return (enemyDirectionLocal.y < 0);
    }

    private void HandleTurretRotation()
    {
        float dotHorizontal = HandleHorizontalRotation();

        if (!IsEnemyBehind() && dotHorizontal > -0.75 && dotHorizontal < 0.75)
            HandleVerticalRotation();
    }

    private void AimAtPlayer()
    {
        HandleTurretRotation();
        DrawDebugShootingRay();

    }

    private void ShootProjectile()
    {

    }
}
