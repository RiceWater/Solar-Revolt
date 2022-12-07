using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject image;
    [SerializeField] private Vector3 offSet;

    private void Update()
    {
        image.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
        costText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
    }

}
