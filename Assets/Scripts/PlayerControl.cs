using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    float horizontalMove;
    public float speed;

    Rigidbody2D myBody;

    bool grounded = false;
    bool flip = false;
    bool dead = false;
    bool win = false;
    bool win2 = false;


    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    public float jumpLimit = 2f;

    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }else if(Input.GetButtonDown("Fire1") && flip == false)
        {
            gravityScale = -3;
            gravityFall = -4;
            flip = true;
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
            
        }
        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        
        if (dead == true)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (win == true)
        {
            SceneManager.LoadScene("Level2");
        }
        if (win2 == true)
        {
            SceneManager.LoadScene("Win1");
        }

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if (hit.collider != null && hit.transform.name == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (hit.collider != null && hit.transform.name == "Spike")
        {
            dead = true;
        }
        if (hit.collider != null && hit.transform.name == "Flag")
        {
            win = true;
        }
        if (hit.collider != null && hit.transform.name == "FlagWIN")
        {
            win2 = true;
        }
        if (hit.collider != null && hit.transform.name == "Circle")
        {
            dead = true;
        }


        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
    /*
    public void LoadGame()
    {
        if (dead == true)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (win == true)
        {
            SceneManager.LoadScene("Win1");
        }
        if (win2 == true)
        {
            SceneManager.LoadScene("Win2");
        }
    }*/
}