using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
  [SerializeField] private string destination;
  private GameManager gameManager;

  private Animator animator;
  public bool canOpen;
  // Start is called before the first frame update
  void Start() {
    animator = GetComponent<Animator>();
    gameManager = FindObjectOfType<GameManager>();
    canOpen = destination != "" ? true : false;
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
    if (canOpen) {
      gameManager.changeScene(destination);
    }
  }
}
