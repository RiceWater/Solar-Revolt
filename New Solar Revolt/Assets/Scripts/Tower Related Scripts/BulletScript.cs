using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Attributes")]
    [SerializeField] private float damage;
    [SerializeField] private float splashRange;
    private GameObject target;
    private Rigidbody2D bulletRigidBody;
    private float moveSpeed;
    private float rotationSpeed;

    private bool teslaTriggered;
    private void Start()
    {
        bulletRigidBody = transform.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 30f;
        rotationSpeed = 4f;
        teslaTriggered = false;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        TravelToTarget();
        CheckOutOfBounds();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(splashRange > 0)
            {
                ApplySplashDamage();
            }
            else if (collision.gameObject.Equals(target))
            {
                target.GetComponent<EnemyAttributesScript>().TakeDamage(damage);
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            }
            Destroy(gameObject);
        }
        //if tesla bullet hits tesla tower, run this    (bullet acts as a catalyst of tower for AoE)
        else if(collision.gameObject.name.Contains("Tesla XM-T50 EHSS") && transform.gameObject.name.Contains("Tesla XM-T50 EHSS Bullet") && !teslaTriggered) 
        {
            teslaTriggered = true;  //to avoid double trigger (tower has 2 trigger colliders)
            ApplyStunAndSplashDamage();
            Destroy(gameObject);
        }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float SplashRange
    {
        get { return damage; }
        set { splashRange = value; }
    }

    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    private void ApplySplashDamage()
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<EnemyAttributesScript>();
            if (enemy)
            {
                var closestPoint = hitCollider.ClosestPoint(transform.position);
                var distance = Vector3.Distance(closestPoint, transform.position);

                var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                enemy.TakeDamage(Mathf.Ceil(damagePercent * damage));
                Destroy(gameObject);
            }
        }
    }

    private void ApplyStunAndSplashDamage()
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<EnemyAttributesScript>();
            if (enemy)
            {
                //Stun
                bool canStun = Random.Range(0, 101) <= 20;  //20% chance to stun enemies
                if (canStun)
                {
                    enemy.gameObject.GetComponent<EnemyMovementScript>().Stun(2f);
                }

                //Damage
                var closestPoint = hitCollider.ClosestPoint(transform.position);
                var distance = Vector3.Distance(closestPoint, transform.position);

                var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                enemy.TakeDamage(Mathf.Ceil(damagePercent * damage));
            }
        }
    }

    private void CheckOutOfBounds()
    {
        int xbound = 50, ybound = 30;
        if (transform.position.x > xbound || transform.position.x < -xbound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > ybound || transform.position.y < -ybound)
        {
            Destroy(gameObject);
        }

    }

    private void TravelToTarget()
    {
        Vector3 dir = (Vector2)target.transform.position - bulletRigidBody.position;
        dir.Normalize();
        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        bulletRigidBody.angularVelocity = -rotationAmount * rotationSpeed;
        bulletRigidBody.velocity = transform.up * moveSpeed;
    }
}
