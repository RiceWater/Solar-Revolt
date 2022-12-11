using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDescriptionForUpgradesScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string name;
    public string fireRateInfo;
    public string damageInfo;
    private float timeToWait = 0.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
        Debug.Log("Here");
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
            name = transform.root.GetComponent<CircleCollider2D>().radius.ToString();
            fireRateInfo = transform.root.GetComponent<TurretScript>().FireRate.ToString();
            damageInfo = transform.root.GetComponent<TurretScript>().BulletDamage.ToString();
        }
        else if (transform.root.GetComponent<TeslaTowerScript>() != null)
        {
            name = transform.root.GetComponent<CircleCollider2D>().radius.ToString();
            fireRateInfo = transform.root.GetComponent<TeslaTowerScript>().FireRate.ToString();
            damageInfo = transform.root.GetComponent<TeslaTowerScript>().BulletDamage.ToString();
        }
        name = "Range: " + name;
        fireRateInfo = "Fire Rate: " + fireRateInfo;
        damageInfo = "Damage per Shot: " + damageInfo;
        HoverDescriptionManagerScript.OnMouseOver(name, fireRateInfo, damageInfo, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
