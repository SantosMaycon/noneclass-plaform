using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private int amountOfJump = 1;
  private Rigidbody2D rigidbody2d;
  private Animator animator;
  private int totalOfJump;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    totalOfJump = amountOfJump;
  }

  // Update is called once per frame
  void Update() {
    move();
    jump();
  }

  private void move() {
    float horizontal = Input.GetAxis("Horizontal");

    if (horizontal != 0f) {
      float directionFlip = Mathf.Sign(horizontal); // return -1 to negative value or 1 to positive value
      transform.localScale = new Vector3(directionFlip, transform.localScale.y, transform.localScale.z); 
    }

    animator.SetBool("isMove", horizontal != 0);

    float velocity = horizontal * speed;
    rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);
  }

  private void jump() {
    animator.SetFloat("verticalVelocity", rigidbody2d.velocity.y);

    if (Input.GetButtonDown("Jump") && amountOfJump > 0) {
      rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
      animator.SetBool("isOnTheFloor", false);
      amountOfJump--;
    }
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.CompareTag("Floor")) {
      animator.SetBool("isOnTheFloor", true);
      amountOfJump = totalOfJump;
    }
  }

  private void OnCollisionExit2D(Collision2D other) {
    if (other.gameObject.CompareTag("Floor")) {
      animator.SetBool("isOnTheFloor", false);
      Debug.Log("Is not on the floor");
    }
  }
}
