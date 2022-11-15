using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBScript : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D bulletRigidBody;
    private float moveSpeed;
    private float rotationSpeed;
    private float damage = 2;
    private float splashRange = 5;

    void Start()
    {
        bulletRigidBody = transform.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 20f;
        rotationSpeed = 4f;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
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
                var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
                foreach(var hitCollider in hitColliders)
                {
                    var enemy = hitCollider.GetComponent<EnemyAttributesScript>();
                    if (enemy)
                    {
                        var closestPoint = hitCollider.ClosestPoint(transform.position);
                        var distance = Vector3.Distance(closestPoint, transform.position);

                        var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                        enemy.TakeDamage(damagePercent * damage);
                        Destroy(gameObject);
                    }
                }
            }
            else if (collision.gameObject.Equals(target))
            {
                target.GetComponent<EnemyAttributesScript>().TakeDamage(1); //replace with tower damage (Wood's Code Needed)
                Destroy(gameObject);
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            }
            Destroy(gameObject);
        }
    }


    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    private void TravelToTarget()
    {
        Vector3 dir = (Vector2)target.transform.position - bulletRigidBody.position;
        dir.Normalize();
        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        bulletRigidBody.angularVelocity = -rotationAmount * rotationSpeed;
        bulletRigidBody.velocity = transform.up * moveSpeed;
    }

    private void CheckOutOfBounds()
    {
        int xbound = 50, ybound = 30;
        if(transform.position.x > xbound || transform.position.x < -xbound)
        {
            Destroy(gameObject);
        }
        else if(transform.position.y > ybound || transform.position.y < -ybound)
        {
            Destroy(gameObject);
        }

    }
}
