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

    public AudioClip gameSong;

    public AudioClip winSong;

    Animator anim;

    private bool facingRight = true;


    //Jumping stuff
    
    private bool isOnGround;
    
    public Transform groundcheck;
    
    public float checkRadius;
    
    public LayerMask allGround;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text ="";
        lives.text = livesValue.ToString();
        musicSource.clip = gameSong;
        musicSource.Play();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
   if (hozMovement != 0) {
      anim.SetInteger("State", 1);
   }
   if (hozMovement == 0){
        anim.SetInteger("State", 0);

   }

  //Jumping stuff

   isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);    
  
  
    }
    void Update()
    {


if (isOnGround == true)
{

    if (Input.GetKeyDown(KeyCode.D))
        {
        anim.SetInteger("State", 1);
        }

    if (Input.GetKeyUp(KeyCode.D))
      {
      anim.SetInteger("State", 0);
      }

    if (Input.GetKeyDown(KeyCode.A))
    {
        anim.SetInteger("State", 1);
    }

    if (Input.GetKeyUp(KeyCode.A))
    {
        anim.SetInteger("State", 0);
    }

    if (Input.GetKeyDown(KeyCode.W))
    {
        anim.SetInteger("State", 3);
    }
}
if (isOnGround == false)
    {
         anim.SetInteger("State", 3);
    }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 8)
            {
                winText.text = "You win! Game created by Joaquin Villalobos.";
                musicSource.Stop();
                musicSource.clip = winSong;
                musicSource.Play();
            }
            if (scoreValue == 4) {
                transform.position = new Vector3(77, 9, 0);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0)
            {
                winText.text = "Sorry, but you lose.";
                gameObject.active = false;
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


   private void OnCollisionStay2D(Collision2D collision)
{
if (collision.collider.tag == "Ground" && isOnGround)
{

if (Input.GetKey(KeyCode.W))
{
rd2d.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
}
}
}
}