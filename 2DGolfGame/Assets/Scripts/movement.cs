using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    // private Animator anim;

    [Header("移动参数")]
    public float speed = 8f;
    public float jumpForce = 6.3f;

    private float horizontalMove;
    // public Transform groundCheck;

    [Header("状态")]
    public bool isJump;
    public bool isOnGround; //是否落地

    [Header("环境监测")]
   public LayerMask groundLayer;

    //碰撞体尺寸
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderBigSize;
    Vector2 colliderBigOffset;


    //按键设置
   bool jumpPressed;
   // int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderBigSize = new Vector2(coll.size.x * 2, coll.size.y * 2);
        colliderBigOffset = new Vector2(coll.size.x * 2, coll.size.y * 2);
       // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpPressed = Input.GetButton("Jump");
            
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();

       // Jump();
        //  SwitchAnim();
    } 


    void PhysicsCheck()
    {
        if(coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
            isJump = false;
        }
        else
        {
            isOnGround = false;
        }
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        
        //人物反转
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove * 1.4f, 1.4f, 1.4f);
        }
        
    }

    void MidAirMovement()
    {
        if (jumpPressed && isOnGround && !isJump)
        {
            isOnGround = false;
            isJump = true;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    /*
    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()//动画切换
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
    */
}
