using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EthanCharacter : MonoBehaviour
{
  public Game reference;
  private Animator animator;
  public Rigidbody rb;
  public float modifier = .5f;
  public float jumpForce = 10;
  [Range(-2, 2)] public float speed = 0;
  public bool jump = false;
    private bool grounded = true;


  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !reference.gameOver) Jump();
    

    //horizontal = speed;

    //Set character rotation
    float y = (horizontal < 0) ? -90 : 90;
        if (!reference.gameOver)
        {
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
            transform.rotation = newRotation;

            //Set character animation
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            //move character
            transform.Translate(-transform.right * horizontal * modifier * Time.deltaTime);

            if(Input.GetKey(KeyCode.LeftShift))
            {
                
                animator.SetFloat("Speed", Mathf.Abs(horizontal) * 1.5f);
                transform.Translate(-transform.right * horizontal * modifier * Time.deltaTime);
            }
        }
  }

  void FixedUpdate()
  {
    
  }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.name == "Brick(Clone)")
        {
            var normal = collision.contacts[0].normal;
            if(normal.y > 0)
            {
                grounded = true;
            }
            if (normal.y < 0)
            {
                
                Destroy(collision.gameObject);
                reference.hitBrick();
            }
        }

        if (collision.gameObject.name == "Floor(Clone)" || collision.gameObject.name == "Stone(Clone)")
            grounded = true;

        if (collision.gameObject.name == "Question(Clone)")
        {
            var normal = collision.contacts[0].normal;
            if (normal.y > 0)
            {
                grounded = true;
            }
            if (normal.y < 0)
            {
                reference.hitQuestion();
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Goal(Clone)")
        {
            reference.gameWin();
            reference.gameOver = true;
            reference.score += 100;
            reference.scoreText.text = reference.score.ToString();
        }
        if (other.gameObject.name == "Barrier(Clone)")
        {
            reference.gameLoss();
            reference.gameOver = true;
        }
    }
}
