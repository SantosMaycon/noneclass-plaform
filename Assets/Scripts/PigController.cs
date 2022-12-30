using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour {
  [SerializeField] private float speed;
  [SerializeField] private float timeToWalk;
  [SerializeField] private LayerMask levelLayer;
  private Rigidbody2D rigidbody2d;
  private Animator animator;
  private BoxCollider2D boxCollider2d;
  [SerializeField] private GameObject collision;
  private bool isStopped = false;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    boxCollider2d = GetComponent<BoxCollider2D>();
    collision = gameObject.transform.GetChild(0)?.gameObject;
  }

  // Update is called once per frame
  void Update() {
    if (!isStopped) {
      move();
    }
  }

  private void FixedUpdate() {
    if (hitTheWall()) {
      rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * -1f, rigidbody2d.velocity.y);
      transform.localScale = new Vector3(Mathf.Sign(rigidbody2d.velocity.x) * -1, 1f, 1f);
    }
  }

  private bool hitTheWall() {
    return Physics2D.Raycast(boxCollider2d.bounds.center, new Vector2(Mathf.Sign(rigidbody2d.velocity.x), 0f), 0.5f, levelLayer);
  }

  private void move() {
    if (timeToWalk <= 0) {
      var direction = Random.Range(-1, 2);
      rigidbody2d.velocity = new Vector2(speed * direction, rigidbody2d.velocity.y);
      animator.SetBool("isMove", rigidbody2d.velocity.x != 0);

      if (rigidbody2d.velocity.x != 0) {
        transform.localScale = new Vector3(Mathf.Sign(rigidbody2d.velocity.x) * -1, 1f, 1f);
      }

      timeToWalk = Random.Range(2f, 8f);
    } else {
      timeToWalk -= Time.deltaTime;
    }
  }

  public void killPig() {
    if (collision && animator) {
      isStopped = true;
      animator.SetTrigger("hit");
      collision.gameObject.SetActive(false);
      rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
      Destroy(gameObject, 2f);
    }
  }
}
