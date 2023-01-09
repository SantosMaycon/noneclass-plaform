using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private int life;
  [SerializeField] private float invincibilityTime;
  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private int amountOfJump = 1;
  [SerializeField] private LayerMask levelLayer;
  [SerializeField] private DoorController door;
  private Rigidbody2D rigidbody2d;
  private BoxCollider2D boxCollider2d;
  private Animator animator;
  private int totalOfJump;
  private float counterInvincibilityTime = 0;
  private bool isStopped = false;
  private GameManager gameManager;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    boxCollider2d = GetComponent<BoxCollider2D>();
    totalOfJump = amountOfJump;
    
    gameManager = FindObjectOfType<GameManager>();
    
    if (gameManager) {
      life = gameManager.getLifeOfPlayer();
    }
  }

  // Update is called once per frame
  void Update() {
    if (!isStopped) {
      move();
      jump();
      onOpenTheDoor();
    }

    if (counterInvincibilityTime > 0f) {
      counterInvincibilityTime -= Time.deltaTime;
    }
  }

  private void FixedUpdate() {
    animator.SetBool("isOnTheFloor", isGrounded());

    if (isGrounded() && rigidbody2d.velocity.y < 0.1f) {
      amountOfJump = totalOfJump;
    }
  }

  private void move() {
    float horizontal = Input.GetAxis("Horizontal");

    if (horizontal != 0f) {
      float directionFlip = Mathf.Sign(horizontal); // return -1 to negative value or 1 to zero or positive value
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

        // Die animation of pig
        other.gameObject.GetComponentInParent<PigController>()?.killPig();
      } else if (counterInvincibilityTime <= 0 && !isStopped) {
        animator.SetTrigger("hit");
        animator.SetInteger("life", --life);
        counterInvincibilityTime = invincibilityTime;

        gameManager?.setLifeOfPlayer(life);
        gameManager?.printHeartsOnScreen();

        if (life <= -1) {
          onStop();
        }
      }
    }

    if (other.CompareTag("Door")) {
      door = other.GetComponent<DoorController>();
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("Door")) {
      door = null;
    }
  }

  private void onStop() {
    isStopped = true;
    rigidbody2d.velocity = new Vector2(0f, rigidbody2d.velocity.y);
  }

  private void onOpenTheDoor() {
    if (Input.GetKeyUp(KeyCode.UpArrow) && rigidbody2d.velocity.x == 0 && door.canOpen) {
      onStop();
      door?.opening();
      Invoke("crossingToTheDoor", 1f);
    }
  }

  private void crossingToTheDoor() {
    animator.SetTrigger("door in");
  }

  public int getLife () {
    return life;
  }

  public void dead() {
    gameManager?.resetGame();
  }
}
