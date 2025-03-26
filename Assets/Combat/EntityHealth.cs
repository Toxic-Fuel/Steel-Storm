using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float MaxHealth;
    public float StartingHealth;
    float health;
    [SerializeField]
    public EffectBuff[] effects;
    public HealthVisualizer healthVisualizer;
    public BasicEnemy enemy;
    void CheckHealth()
    {
        if(health <= 0)
        {
            enemy.OnDeath();
        }
        healthVisualizer.ChangeHealth(health);
    }
    public void TakeDamage(float damage, string effectName)
    {
        float currentDamage = damage;
        float stackedEffects = 1;
        foreach (EffectBuff effect in effects)
        {
            if(effect.EffectName == effectName)
            {
                stackedEffects += effect.Effectivness;
                
            }
        }
        currentDamage *= stackedEffects;
        health-=currentDamage;
        enemy.Damaged();
    }
}
