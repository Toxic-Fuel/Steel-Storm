using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float MaxHealth;
    public float StartingHealth;
    float health;
    
    public EffectBuff[] effects;
    public HealthVisualizer healthVisualizer;
    public BasicEnemy enemy;
    public GameObject DamagePopUp;


    private void Start()
    {
        health = StartingHealth;
    }
    void CheckHealth()
    {
        if(health <= 0)
        {
            enemy.OnDeath();
        }
        if(healthVisualizer != null)
        {
            healthVisualizer.ChangeHealth(health);
        }
        
    }
    public void TakeDamage(float damage, string[] effectNames = null)
    {
        float currentDamage = damage;
        float stackedEffects = 1;
        if(effectNames != null && effects != null)
        {
            foreach (EffectBuff effect in effects)
            {
                for (int i = 0; i < effectNames.Length; i++)
                {
                    if (effect.EffectName == effectNames[i])
                    {
                        stackedEffects += effect.Effectivness;
                        break;
                    }
                }

            }
        }
        
        currentDamage *= stackedEffects;
        health-=currentDamage;
        GameObject popUp = Instantiate(DamagePopUp, transform.position + UnityEngine.Random.insideUnitSphere * 1f, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = "-" + currentDamage;
        enemy.Damaged();
        CheckHealth();
    }
}
