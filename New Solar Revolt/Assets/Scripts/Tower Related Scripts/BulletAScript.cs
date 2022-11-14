using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAScript : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D bulletRigidBody;
    private float moveSpeed;
    private float rotationSpeed;

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
            return;
        }
        TravelToTarget();
        CheckOutOfBounds();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.Equals(target))
            {
                target.GetComponent<EnemyAttributesScript>().TakeDamage(1); //replace with tower damage (Wood's Code Needed)
                Destroy(gameObject);
            }
            else
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            }
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
