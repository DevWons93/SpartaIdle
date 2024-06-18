using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action<Health> OnDieEvent;

    public bool IsDie = false;

    private void Start()
    {
        health = maxHealth;
        IsDie = false;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            IsDie = true;
            OnDieEvent?.Invoke(this);
            Destroy(gameObject);
        }

        Debug.Log(health);
    }
}
