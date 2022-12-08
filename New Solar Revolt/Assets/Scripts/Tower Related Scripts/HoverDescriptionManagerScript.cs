using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class HoverDescriptionManagerScript: MonoBehaviour
{
    public TextMeshProUGUI towerDesc;
    public RectTransform descWindow;

    public static UnityAction<string, Vector2> OnMouseOver;
    public static UnityAction OnMouseLoseFocus;

    private void Start()
    {
        HideDescription();
    }

    private void OnEnable()
    {
        OnMouseOver += ShowDescription;
        OnMouseLoseFocus += HideDescription;
    }

    private void OnDisable()
    {
        OnMouseOver -= ShowDescription;
        OnMouseLoseFocus -= HideDescription;
    }

    private void ShowDescription(string desc, Vector2 mousePos)
    {
        descWindow.gameObject.SetActive(true);
        towerDesc.text = desc;
        descWindow.transform.position = new Vector2(mousePos.x + descWindow.sizeDelta.x * 2/ 3, mousePos.y);
        Debug.Log(mousePos.x + descWindow.sizeDelta.x);
    }
    private void HideDescription()
    {
        towerDesc.text = default;
        descWindow.gameObject.SetActive(false);
    }
}
