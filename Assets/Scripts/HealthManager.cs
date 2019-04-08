using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public event Action CriticalDamageTaken;
    public Damager damager;
    public float healthMaxValue;
    public float health;

    void Start()
    {
        health = healthMaxValue;
        damager.TakingDamageEvent += TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            CriticalDamageTaken?.Invoke();
            health = healthMaxValue;
        }
    }
}
