using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDescriptionScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string input1;
    public string fireRateInfo;
    public string damageInfo;
    private float timeToWait = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverDescriptionManagerScript.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        HoverDescriptionManagerScript.OnMouseOver(input1, fireRateInfo, damageInfo, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
