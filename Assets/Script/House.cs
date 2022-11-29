using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] int health;
    void Start()
    {
        
    }

    void Update()
    {
        Death();
    }
    void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void GetDamage(int damage)
    {
        health -= damage;
    }
}
