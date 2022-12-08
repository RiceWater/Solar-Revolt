using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDescriptionScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string towerDescriptionToShow;
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
        Debug.Log("Message Showed");
        HoverDescriptionManagerScript.OnMouseOver(towerDescriptionToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        Debug.Log("Message 1");
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("Message 2");
        ShowMessage();
    }
}
