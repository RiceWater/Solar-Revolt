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
        moveSpeed = 10f;
        rotationSpeed = 4f;
    }

    void Update()
    {
        //Code for traveling towards the target 
        Vector3 dir =  (Vector2)target.transform.position - bulletRigidBody.position;
        dir.Normalize();
        float rotationAmount = Vector3.Cross(dir, transform.up).z;
        bulletRigidBody.angularVelocity = -rotationAmount * rotationSpeed;
        bulletRigidBody.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.Equals(target))
            {
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


}
