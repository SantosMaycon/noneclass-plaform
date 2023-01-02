using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
  [SerializeField] private string destination;
  private GameManager gameManager;

  private Animator animator;
  // Start is called before the first frame update
  void Start() {
    animator = GetComponent<Animator>();
    gameManager = FindObjectOfType<GameManager>();
  }

  // Update is called once per frame
  void Update() {}

  public void opening() {
    animator?.SetTrigger("open");
  }

  public void closing() {
    animator?.SetTrigger("close");
  }

  public void onDestination() {
    if (destination != null) {
      gameManager.changeScene(destination);
    }
  }
}
