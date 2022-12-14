using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator bulletAnimator;
    [SerializeField] private BulletScript bulletScript;

    private void Update()
    {
        if (bulletScript.HasHitEnemy)
        {
            bulletAnimator.SetBool("hasReachedEnemy", true);
            Invoke("KillObject", 0.2f);
        }
    }

    private void KillObject()
    {
        Destroy(transform.gameObject);
    }
}
