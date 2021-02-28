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
  public float jumpForce = 5;
  [Range(-2, 2)] public float speed = 0;
  public bool jump = false;


  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
    jump = (Input.GetKeyDown(KeyCode.Space)) ? true : false;

    //horizontal = speed;

    //Set character rotation
    float y = (horizontal < 0) ? -90 : 90;
    Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    transform.rotation = newRotation;

    //Set character animation
    animator.SetFloat("Speed", Mathf.Abs(horizontal));

    //move character
    transform.Translate(-transform.right * horizontal * modifier* Time.deltaTime);
    
  }

  void FixedUpdate()
  {
    if (jump) rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
  }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Brick(Clone)")
        {
            Destroy(collision.gameObject);
            reference.hitBrick();
        }
    }
}
