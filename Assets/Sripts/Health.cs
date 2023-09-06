using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour,IDamageable
{
    [SerializeField] private int _MaxHealth = 200;
    [SerializeField] private int _health = 200;



    public void TakeDamage(float damage)
    {
        _health -= (int)damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Death");
        gameObject.SetActive(false);
    }

  
    void Start()
    {
        _health = _MaxHealth;
    } 
}
