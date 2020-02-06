using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    private CircleCollider2D coll;
    private Transform GP;
    
    // private Animator anim;

    [Header("移动参数")]
    public float speed = 7f;
    public float jumpForce = 6.3f;
    private float horizontalMove;
    //private float currentSize;

    [Header("状态")]
    public bool isJump;
    public bool isOnGround; //是否落地

    [Header("环境监测")]
    public LayerMask groundLayer;

    //碰撞体尺寸
    float colliderStandRadius;
    Vector2 colliderStandOffset;
    //Vector2 colliderSmallSize;
    Vector2 colliderSmallOffset;

    //角色尺寸
    Vector3 golfStandSize;
    Vector3 golfStandPos;
    Vector3 golfSmallSize;
    //Vector3 golfSmallPos;


    //按键设置
    bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        GP = GetComponent<Transform>();

        colliderStandRadius = coll.radius;
        colliderStandOffset = coll.offset;
        //colliderSmallSize = new Vector2(coll.size.x , coll.size.y );
        colliderSmallOffset = new Vector2(coll.offset.x / 2.5f , coll.offset.y / 2.5f);
        golfStandSize = GP.localScale;
        golfStandPos = GP.position;
        golfSmallSize = new Vector3( GP.localScale.x / 2.5f, GP.localScale.y / 2.5f, GP.localScale.z / 2.5f);
       // golfSmallPos = new Vector3( GP.position.x + 0.2f, GP.position.y, GP.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        jumpPressed = Input.GetButton("Jump");
            
    }

    private void FixedUpdate()
    {
        //PhysicsCheck();
        //GroundMovement();
        //MidAirMovement();

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
        
        ////人物反转
        //if (horizontalMove != 0)
        //{
        //    transform.localScale = new Vector3(horizontalMove * System.Math.Abs(GP.localScale.x), GP.localScale.y, GP.localScale.z);
        //}
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集
        if(collision.tag == "Collection")
        {
            Destroy(collision.gameObject);

            coll.offset = colliderSmallOffset;
            GP.localScale = golfSmallSize;            //transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f);

        }

        //
        if (collision.tag == "GameOver")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
