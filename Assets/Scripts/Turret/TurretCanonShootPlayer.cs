using UnityEngine;
using System.Collections.Generic;

public class TurretCanonShootPlayer : MonoBehaviour
{
    public GameObject target;
    public GameObject projectile;
    public GameObject projectileSpawnLocation;
    public float aimingAccuracy = 0.10f;
    public float rotationSpeed = 60;
    public float delayBetweenShots = 3f;
    private float lastTimeShot = 0f;
    private GameObject turretBody;
    private Vector3 predictedTargetPosition;
    private float projectileTimeToTarget = 1f;
    private int projectileSpeed = 10;
    private float distanceToTarget = 0f;

    void Start()
    {
        turretBody = transform.parent.gameObject;
        predictedTargetPosition = target.transform.position;
        projectileSpeed = projectile.GetComponentInChildren<HandleProjectileCollisions>().getProjectileSpeed();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        projectileTimeToTarget = distanceToTarget / projectileSpeed;
        predictedTargetPosition = target.GetComponent<MovementChecker>().PredictFuturePosition(projectileTimeToTarget);
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
        Vector3 toOther = predictedTargetPosition - transform.position;
        float rotation = 0;
        float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.right), toOther);

        if (dotProduct < aimingAccuracy && dotProduct > -(aimingAccuracy))
            return(dotProduct);
        if (dotProduct > 0)
            rotation = rotationSpeed;
        else
            rotation = -rotationSpeed;
        transform.RotateAround(turretBody.transform.position, Vector3.up, rotation * Time.deltaTime);
        return(dotProduct);
    }

    private float HandleVerticalRotation()
    {
        Vector3 toOther = target.transform.position - transform.position;
        float rotation = 0;
        float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.forward), toOther);

        if (dotProduct < aimingAccuracy && dotProduct > -(aimingAccuracy))
            return(dotProduct);
        if (dotProduct > 0)
            rotation = rotationSpeed;
        else
            rotation = -rotationSpeed;
        transform.Rotate(Vector3.right, rotation * Time.deltaTime);
        return (dotProduct);
    }

    private bool IsEnemyBehind()
    {
        Vector3 enemyDirectionLocal = transform.InverseTransformPoint(predictedTargetPosition);

        return (enemyDirectionLocal.y < 0);
    }

    private void HandleTurretRotation()
    {
        float dotHorizontal = HandleHorizontalRotation();
        float dotVertical = -1;
        bool readyHorizontal = false;
        bool readyVertical = false;

        if (readyHorizontal = (!IsEnemyBehind() && dotHorizontal > -(aimingAccuracy) && dotHorizontal < aimingAccuracy))
            dotVertical = HandleVerticalRotation();
        
        readyVertical = (!IsEnemyBehind() && dotVertical > -(aimingAccuracy) && dotVertical < aimingAccuracy);
        if (readyHorizontal && readyVertical)
            ShootProjectile();
    }

    private void AimAtPlayer()
    {
        HandleTurretRotation();
    }

    private void ShootProjectile()
    {
        if (lastTimeShot == 0 || Time.time - delayBetweenShots > lastTimeShot) {
            Instantiate(projectile, projectileSpawnLocation.transform.position, projectileSpawnLocation.transform.rotation);
            lastTimeShot = Time.time;
        }
    }
}
