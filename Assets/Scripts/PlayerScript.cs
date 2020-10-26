using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    
    Animator anim; 
   

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winText.text = "";
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }
        void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
            if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
            else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        anim.SetInteger("State", 0);

        if (hozMovement !=0)
        {
            anim.SetInteger("State", 1);
        }
        if (verMovement !=0)
        {
        anim.SetInteger("State", 2);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        
            if (scoreValue == 9)
            {
                winText.text = "You Win! Game by: Will Combass";
                {
                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                }
            }

            if (scoreValue == 4)
            {
                transform.position = new Vector2 (45.0f, 0);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -=1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);

            if (livesValue == 0)
            {
                Destroy (this.gameObject);
                winText.text = "You Lose! Game by: Will Combass";
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "Ground" && isOnGround)
        {
        if (Input.GetKey(KeyCode.W))
        {
        rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
        }
        
    }
}
