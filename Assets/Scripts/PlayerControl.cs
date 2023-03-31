using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

public class PlayerControl : MonoBehaviour
{
    public SpriteRenderer spi;
    public Animator animator;
    public RuntimeAnimatorController newController;

    float horizontalMove;
    public float speed;

    Rigidbody2D myBody;

    bool grounded = false;
    bool flip = false;
    bool dead = false;
    bool win = false;
    bool win2 = false;
    bool win3 = false;
    bool moveRight = false;
    bool moveLeft = false;
    bool moveRightU = false;
    bool moveLeftU = false;
    bool Up = false;

    public GameObject id1;
    public GameObject id2;
    public GameObject id3;
    public GameObject id4;
    public GameObject idbox;

    public int id = 0;
    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    public float jumpLimit = 2f;

    bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        spi = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Up"))
        {
            Up = true;
        }
        if (dead == true)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (win == true)
        {
            SceneManager.LoadScene("Level3");
        }
        if ((win3 == true))
            {
                SceneManager.LoadScene("Level2");
            }
        
        if (win2 == true)
        {
            SceneManager.LoadScene("Win1");
        }

        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            //Debug.Log(jump);
        }
        //Debug.Log(horizontalMove);

        if (Input.GetButtonDown("Fire1") && flip == true)
        {
            gravityScale = 3;
            gravityFall = 4;
            flip = false;
            spi.flipY = false;

        }
        else if (Input.GetButtonDown("Fire1") && flip == false)
        {
            gravityScale = -3;
            gravityFall = -4;
            flip = true;
            spi.flipY = true;
        }
        //scene changes for win/loss

    }
    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
            animator.SetBool("jump", true);

        }
        else if (horizontalMove > 0)
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false);
            spi.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false);
            spi.flipX = true;
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", true);
        }


      

        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if (hit.collider != null && hit.transform.tag == "ground")
        {
            grounded = true;
            animator.SetBool("jump", false);
        }
        else
        {
            grounded = false;
        }

        if (hit.collider != null && hit.transform.tag == "spike")
        {
            dead = true;
        }
        if ((hit.collider != null && hit.transform.name == "Flag") && (Up == true))
        {
            win = true;
        }
        if ((hit.collider != null && hit.transform.name == "Flag2") && (Up == true))
        {
            win3 = true;
        }
        if ((hit.collider != null && hit.transform.tag == "FlagWIN"))
        {
            win2 = true;
        }
        if (hit.collider != null && hit.transform.name == "Circle")
        {
            dead = true;
        }

        if ((hit.collider != null && hit.transform.tag == "id1"))
        {
            id += 1;
            Destroy(id1);
        }
        if ((hit.collider != null && hit.transform.name == "id2"))
        {
            id += 1;
            Destroy(id2);
        }
        if ((hit.collider != null && hit.transform.name == "id3"))
        {
            id += 1;
            Destroy(id3);
        }
        if ((hit.collider != null && hit.transform.name == "id4"))
        {
            id += 1;
            Destroy(id4);
        }
        if(id == 4)
        {
            Destroy(idbox);
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
}