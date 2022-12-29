using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private int amountOfJump = 1;
  [SerializeField] private BoxCollider2D boxCollider2d;
  [SerializeField] private LayerMask levelLayer;
  private Rigidbody2D rigidbody2d;
  private Animator animator;
  private int totalOfJump;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    boxCollider2d = GetComponent<BoxCollider2D>();
    totalOfJump = amountOfJump;
  }

  // Update is called once per frame
  void Update() {
    move();
    jump();
  }

  private void FixedUpdate() {
    animator.SetBool("isOnTheFloor", isGrounded());

    if (isGrounded()) {
      amountOfJump = totalOfJump;
    }
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

  private bool isGrounded() {
    return Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, 0.5f, levelLayer);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Enemy")) {
      var hitTheHead = transform.position.y > other.transform.position.y + .2f;
      if (hitTheHead) {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
        animator.SetBool("isOnTheFloor", false);
      }
    }
  }
}
