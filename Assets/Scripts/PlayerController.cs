using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField] private float speed;
  private Rigidbody2D rigidbody2d;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    move();
  }

  private void move () {
    float horizontal = Input.GetAxis("Horizontal");
    float velocity = horizontal * speed;
    rigidbody2d.velocity = new Vector2(velocity, rigidbody2d.velocity.y);
  }
}
