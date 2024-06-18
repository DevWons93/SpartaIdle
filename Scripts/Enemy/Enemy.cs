using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health Health { get; private set; }

    public Player Target { get; set; }

    private void Start()
    {
        Health = GetComponent<Health>();
        Health.OnDieEvent += Target.RemoveTarget;
        Health.OnDieEvent += GameManager.Instance.RemoveEnemy;
    }

    private void OnDestroy()
    {
        Health.OnDieEvent -= Target.RemoveTarget;
        Health.OnDieEvent -= GameManager.Instance.RemoveEnemy;
    }
}
