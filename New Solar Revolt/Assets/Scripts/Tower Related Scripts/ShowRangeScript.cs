using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRangeScript : MonoBehaviour
{
    [SerializeField] private GameObject towerRangeDisplay;

    private void Start()
    {
        Color spriteColor = towerRangeDisplay.GetComponent<SpriteRenderer>().material.color;
        spriteColor.a = 0.5f;
        towerRangeDisplay.SetActive(false);
    }

    private void Update()
    {
        Vector3 towerRadius = new Vector3(transform.GetComponent<CircleCollider2D>().radius, transform.GetComponent<CircleCollider2D>().radius, 1);
        towerRangeDisplay.transform.localScale = towerRadius;

    }
}
