using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Karakter
{   
    [SerializeField] float playerCollide;
    public Transform weaponHolder;
    public Transform centerPoint;
    [SerializeField] GameObject ball;
    [SerializeField] float ballPowerThrow;
    public static PlayerControl pc;
    float direction;
    bool canRunning = true;
    Rigidbody2D rb;
    bool grounded;
    public static bool canThrow = true;

#region Unity Method
    private void Awake()
    {
        PlayerBind();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3,6,true);
    }
    private void Update()
    {
        Run();
        Flip();
        AnimationControl();
        CheckGround();
    }
#endregion

#region Custom Method
    void Flip()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        if(direction > 0){sp.flipX = false;}
        else if(direction < 0){sp.flipX = true;}
    }
    void AnimationControl()
    {
        var anim = GetComponent<Animator>();
        anim.SetBool("Run",direction != 0);
        anim.SetFloat("Airing",rb.velocity.y);
        anim.SetBool("Grounded",grounded);
    }
    void CheckGround()
    {
        var box = GetComponent<BoxCollider2D>();
        if(box.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            grounded = true;
        }
        else{grounded = false;}
    }
#endregion

#region Player Input 
    void PlayerBind()
    {
        pc = new PlayerControl();
        pc.Player.Enable();
        pc.Player.Jump.started += Jump;
        pc.Player.Shoot.started += Shoot;
    }
    void Run()
    {
        direction = pc.Player.Move.ReadValue<float>();
        if (canRunning)
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

    }
    void Jump(InputAction.CallbackContext context)
    {
        if(context.started && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
    }
    void Shoot(InputAction.CallbackContext context)
    {
        if(context.started && canThrow)
        {
            GetComponent<Animator>().SetTrigger("Throw");
            Ball.rb = ball.AddComponent<Rigidbody2D>();
            Ball.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            canThrow = false;
            Physics2D.IgnoreLayerCollision(0,6,false);
            Physics2D.IgnoreLayerCollision(6,7,false);
            StartCoroutine(BallShoot());
            var mousePos = pc.Player.MousePos.ReadValue<Vector2>();
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            var ballDir = (mousePos - (Vector2)transform.position).normalized * ballPowerThrow;
            var aim = FindObjectOfType<Aim>();
            var ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.gravityScale = 1;
            ball.transform.rotation = Quaternion.Euler(0,0,aim.rotz);
            ball.transform.parent = null;
            ballRb.velocity = new Vector2(ballDir.x,ballDir.y);
        }
        else if(context.started && !canThrow)
        {
            Ball.ballBack = true;
        }
    }
    IEnumerator BallShoot()
    {
        yield return new WaitForSeconds(playerCollide);
        Physics2D.IgnoreLayerCollision(3,6,false);
    }
#endregion
}
