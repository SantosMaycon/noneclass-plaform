using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private float speed;
  private Rigidbody2D rigidbody2d;
  private Animator animator;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    move();
  }

  private void move () {
    float horizontal = Input.GetAxis("Horizontal");

    if (horizontal != 0f) {
      float directionFlip = Mathf.Sign(horizontal); // return -1 to negative value or 1 to positive value
      transform.localScale = new Vector3(directionFlip, transform.localScale.y, transform.localScale.z); 
    }

    animator.SetBool("isMove", horizontal != 0);

    float velocity = horizontal * speed;
    rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);
  }
}
