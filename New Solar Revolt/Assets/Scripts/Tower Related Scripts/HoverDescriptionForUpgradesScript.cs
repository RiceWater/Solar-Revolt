using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDescriptionForUpgradesScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        if(transform.root.GetComponent<TurretScript>() != null)
        {
            input1 = transform.root.GetComponent<CircleCollider2D>().radius.ToString();
            fireRateInfo = transform.root.GetComponent<TurretScript>().FireRate.ToString();
            damageInfo = transform.root.GetComponent<TurretScript>().BulletDamage.ToString();
        }
        else if (transform.root.GetComponent<TeslaTowerScript>() != null)
        {
            input1 = transform.root.GetComponent<CircleCollider2D>().radius.ToString();
            fireRateInfo = transform.root.GetComponent<TeslaTowerScript>().FireRate.ToString();
            damageInfo = transform.root.GetComponent<TeslaTowerScript>().BulletDamage.ToString();
        }
        input1 = "Range: " + input1;
        fireRateInfo = "Fire Rate: " + fireRateInfo;
        damageInfo = "Damage per Shot: " + damageInfo;
        HoverDescriptionManagerScript.OnMouseOver(input1, fireRateInfo, damageInfo, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
