using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Karakter
{
    [SerializeField] int damage;
    [SerializeField] GameObject house;
    bool canWalk = true;
    public void GetDamage(int damage)
    {
        health -= damage;
    }
    private void Update() {
        Death();
        GoToHouse();
    }
    void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void GoToHouse()
    {
        var target = house.transform.position;
        Vector3 fromLeft = new Vector3(target.x,transform.position.y,target.z);
        Vector3 fromRIght = new Vector3(target.x,transform.position.y,target.z);
        if(canWalk)
        {
            if(transform.position.x - target.x < 0)
            {
                transform.localScale = new Vector3(1,1,1);
                transform.position = Vector3.MoveTowards(transform.position,fromLeft,movSpeed * Time.deltaTime);
            }
            else if(transform.position.x - target.x > 0)
            {
                transform.localScale = new Vector3(-1,1,1);
                transform.position = Vector3.MoveTowards(transform.position,fromRIght,movSpeed * Time.deltaTime);
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "House")
        {
            var house = other.gameObject.GetComponent<House>();
            house.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
