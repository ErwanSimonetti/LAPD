using UnityEngine;
using System.Collections.Generic;

public class TurretCanonShootPlayer : MonoBehaviour
{
    public GameObject playerTarget;
    public GameObject projectile;
    public GameObject projectileSpawnPosition;
    public int projectileSpeed = 10;
    public float aimingAccuracy = 0.10f;
    public float rotationSpeed = 60;
    public float delayBetweenShots = 3f;
    private float lastTimeShot = 0f;
    private GameObject turretBody;
    private List<GameObject> projectilesShot = new List<GameObject>(); // maybe not use a list and handle all projectiles with component atteched to them

    void Start()
    {
        turretBody = transform.parent.gameObject;
    }

    void Update()
    {
        AimAtPlayer();
        HandleProjectilesShot();
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

        if (dotProduct > 0)
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

        if (dotProduct > 0)
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

    private void HandleProjectilesShot()
    {
        for (int i = 0; i < projectilesShot.Count; i += 1) {
            projectilesShot[i].transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }
    }

    private void ShootProjectile()
    {
        if (lastTimeShot == 0 || Time.time - delayBetweenShots > lastTimeShot) {
            projectilesShot.Add(Instantiate(projectile, projectileSpawnPosition.transform.position, projectileSpawnPosition.transform.rotation));
            lastTimeShot = Time.time;
        }
    }
}
