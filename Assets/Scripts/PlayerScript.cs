using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    public Text lose;
    public Text lives;
    public bool isGrounded;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool facingRight = true;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        win.text = "";
        lose.text = "";
    }

    void FixedUpdate()
  {
   isGrounded = Physics2D.OverlapCircle (groundcheck.position, checkRadius,whatIsGround);
   float hozMovement = Input.GetAxis("Horizontal");
   float vertMovement = Input.GetAxis("Vertical");
   rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
         else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
          anim.SetInteger("State", 1);
         }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
         {
          anim.SetInteger("State", 0);
         }
        if (Input.GetKeyDown(KeyCode.RightArrow))
         {
          anim.SetInteger("State", 1);
         }
        if (Input.GetKeyUp(KeyCode.RightArrow))
         {
          anim.SetInteger("State", 0);
         }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            anim.SetBool("Jump", false);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
         {
          anim.SetBool("Jump", true);
         }

        if (Input.GetKey("escape"))
        {
         Application.Quit();
        }
  }
    void Update()
    {
        if (isGrounded == true)
        {
            anim.SetBool("Jump", false);
        }
        if (isGrounded == false)
        {
            anim.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector3 (27.0f, 1);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }

            if (scoreValue == 8)
            {
                win.text ="You Win! Created by Matthew Falconett!";
                {
                    musicSource.clip = musicClipOne;
                    musicSource.Stop();
                    
                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                    
                }
            }
        }

       if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            
            if (livesValue == 0)
            {
                lose.text ="You Lose! Created by Matthew Falconett!";
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
     }
    
}