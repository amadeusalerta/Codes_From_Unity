using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretRotation : MonoBehaviour
{
    [Header("Ortak")]
    public List<string> targetTags = new List<string>();

    // Shooter
    [Header("Shooter")]
    public float fireRate;
    public float bulletDamage;
    public float bulletSpeed;
    public float bulletLifeTime;

    public GameObject bulletPrefab;
    public List<ParticleSystem> shootSmokes = new List<ParticleSystem>();
    public List<Transform> firePoints = new List<Transform>();

    private float _fireRate;

    public virtual void StartShooting()
    {
        if (Time.time < _fireRate)
            return;

        _fireRate = Time.time + 1 / fireRate;

        for (int i = 0; i < firePoints.Count; i++)
        {
            ShootBullet(i, bulletDamage, bulletSpeed, bulletLifeTime);
        }
    }

    public virtual void ShootBullet(int i, float bulletDamage, float bulletSpeed, float bulletLifeTime)
    {
        if(shootSmokes.Count>0)shootSmokes[i].Play();
        GameObject bulletGO = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        Physics.IgnoreCollision(bulletGO.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.damage = bulletDamage;
        bullet.targetTags = targetTags;
        bullet.lifeTime = bulletLifeTime;
        bulletGO.GetComponent<Rigidbody>().AddForce(bulletGO.transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bulletGO, bulletLifeTime);
    }
    // Shooter

    // TurretRotation
    [Header("Turret Rotation")]
    public Transform turret;
    public float rotateSpeed;
    public SpriteRenderer rangeSprite;

    public float autoRotationSpeed;

    public bool turnOnly;

    void Start()
    {
        // Turret Rotation - Range
        if (rangeSprite != null && CompareTag("Ally"))
        {
            rangeSprite.transform.localScale = Vector3.one * 2 * shootingRange;
            rangeSprite.gameObject.SetActive(true);
        }

        // Spotting
        StartCoroutine(SelectTarget());
        StartCoroutine(StartScanning());
    }

    void FixedUpdate()
    {
        if (targets.Count > 0 && targets[0] != null)
        {
            // Ona do�ru bak���m�z�n y�n� ve aram�zdaki mesafe
            Vector3 dir = (targets[0].transform.position - transform.position).normalized;
            float dist = Vector3.Distance(transform.position, targets[0].transform.position);

            // Ona s�kacak kadar yak�n�z ve aram�zda engel yok
            if (dist <= shootingRange && !Physics.BoxCast(transform.position, Vector3.one * raycastPathSize, dir, transform.rotation, shootingRange, obsactleMask))
            {
                if (Vector3.Angle(turret.forward, dir) > shootingRange)
                    turnOnly = true;
                else
                    turnOnly = false;

                if (!turnOnly)
                {
                    StartShooting();
                }

                RotateTurret(turret, targets[0].gameObject, rotateSpeed);
            }

            else
            {
                if (Vector3.Angle(turret.forward, dir) > range)
                    turnOnly = true;
                else
                    turnOnly = false;

                RotateTurret(turret, targets[0].gameObject, rotateSpeed);
            }

        }
        else
        {
            AutoTurretRotation(turret, autoRotationSpeed);
        }
    }

    public void RotateTurret(Transform turret, GameObject target, float rotateSpeed)
    {
        Vector3 direction = target.transform.position - turret.position;
        direction.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(direction);

        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRot, rotateSpeed * Time.fixedDeltaTime);
    }

    public void AutoTurretRotation(Transform turret, float autoRotateSpeed)
    {
        Vector3 direction = turret.right;
        direction.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(direction);

        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRot, autoRotateSpeed * Time.fixedDeltaTime);
    }

    public void ResetTurret(Transform turret, float rotateSpeed)
    {
        Vector3 direction = transform.forward;
        direction.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(direction);

        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRot, rotateSpeed * Time.fixedDeltaTime);
    }
    // Turret Rotation

    // Spotter
    [Header("Spotter")]
    public float shootingRange;
    public float attackFov;
    public float range;
    public float raycastPathSize;
    public LayerMask targetMask;
    public LayerMask obsactleMask;
    private List<GameObject> targets = new List<GameObject>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    public IEnumerator SelectTarget()
    {
        for (; ; )
        {
            RemoveDestroyedTargets();

            if (targets.Count > 0 && targets[0] != null)
            {
                float dist = Vector3.Distance(transform.position, targets[0].transform.position);

                Vector3 dir = (targets[0].transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, dir, dist, obsactleMask))
                {
                    if (targets.Count > 1)
                    {
                        for (int i = 0; i < targets.Count; i++)
                        {
                            GameObject temp = targets[i];
                            int randomIndex = UnityEngine.Random.Range(i, targets.Count);
                            targets[i] = targets[randomIndex];
                            targets[randomIndex] = temp;
                        }
                    }
                    else
                    {
                        targets.Remove(targets[0]);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void RemoveDestroyedTargets()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null || Vector3.Distance(transform.position, targets[i].transform.position) > range)
                targets.Remove(targets[i]);
        }
    }

    public IEnumerator StartScanning()
    {
        for (; ; )
        {
            // OnTriggerEnter

            RemoveDestroyedTargets();

            Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, range, targetMask); // Remove Targets If Cant See
            for (int i = 0; i < targetsInRadius.Length; i++)
            {
                Transform _target = targetsInRadius[i].transform;
                if (!targets.Contains(_target.gameObject) && !targetsInRadius[i].isTrigger && targetTags.Contains(_target.tag))
                {
                    targets.Add(_target.gameObject);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
    // Spotter
}
