using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    public Rigidbody2D player;
    public float speed;
    public float Jumpspeed;
    public Animator anim;
    public LayerMask ground;
    public Collider2D PlayerColl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        PlayerJump();
        SwitchAnim();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    void PlayerMovement()  
    {
        var horizontalmove = Input.GetAxis("Horizontal");
        var facerection = Input.GetAxisRaw("Horizontal");

        if(horizontalmove != 0)  //左右移动
        {
            player.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, player.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(facerection));
            
        }
        if (facerection != 0)  //左右转向
        {
            transform.localScale = new Vector3(facerection, 1, 1);
        }
    }


    /// <summary>
    /// 玩家跳跃
    /// </summary>
    void PlayerJump()  
    {
        if (Input.GetButtonDown("Jump") && PlayerColl.IsTouchingLayers(ground) == true)
        {
            player.velocity = new Vector2(player.velocity.x, Jumpspeed );
            anim.SetBool("Jumping", true);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jumping"))
        {
            if(player.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        else if(PlayerColl.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }
    }

}
