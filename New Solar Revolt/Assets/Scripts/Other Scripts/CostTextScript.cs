using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject mouseDetector;
    [SerializeField] private Vector3 offSet;

    private void Update()
    {
        if(mouseDetector != null)
        {
            mouseDetector.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
        }
        if(image != null)
        {
            image.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
        }
        if(costText != null)
        {
            costText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
        }
        
        
    }

}
