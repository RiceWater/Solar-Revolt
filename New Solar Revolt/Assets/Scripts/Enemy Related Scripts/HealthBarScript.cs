using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offSet;

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.root.position + offSet);
    }

    public void SetHealthBar(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.maxValue = maxHealth;
        slider.value = health;
    }
}
