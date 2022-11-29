using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] int maxDamage;
    [SerializeField] PhysicsMaterial2D bouncy;
    public static Rigidbody2D rb;
    public static bool ballBack = false;
    int damage = 1;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 6, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }
    private void Update() 
    {
        Vector2 back = new Vector2(0,0);
        if(rb != null)
        {
            back = rb.velocity;
        }
        if(ballBack)
        {
            Physics2D.IgnoreLayerCollision(0, 6, true);
            Physics2D.IgnoreLayerCollision(6, 7, true);
            var target = FindObjectOfType<Player>().transform.position;
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
            rb.velocity = Vector2.zero;
            if(transform.position.x - target.x > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position,target,20 * Time.deltaTime);
            }
            else if(transform.position.x - target.x < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position,target,20 * Time.deltaTime);
            }
        }
        else if(back.x >= maxSpeed.x || back.y >= maxSpeed.y)
        {
            ballBack = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            damage = 1;
            Player.canThrow = true;
            ballBack = false;
            GetComponent<CircleCollider2D>().sharedMaterial = bouncy;
            Physics2D.IgnoreLayerCollision(0, 6, true);
            Physics2D.IgnoreLayerCollision(3, 6, true);
            Player player = FindObjectOfType<Player>();
            transform.parent = player.centerPoint;
            transform.position = player.weaponHolder.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(GetComponent<Rigidbody2D>());
        }
        if(other.gameObject.tag == "Wall" && damage < maxDamage)
        {
            damage++;
        }
        if(other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(damage);
            damage = 1;
        }

    }
}
