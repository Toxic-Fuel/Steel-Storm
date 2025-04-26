using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisualizer : MonoBehaviour
{
    public TMP_Text HealthText;
    public Slider HealthSlider;

    public void ChangeHealth(float health)
    {
        if (HealthText != null)
        {
            HealthText.text = health.ToString();
        }
        if (HealthSlider != null)
        {
            HealthSlider.value = health;
        }
    }
}
