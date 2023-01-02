using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
  [SerializeField] private Animator animator;
  // Start is called before the first frame update
  void Start() {
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {}

  public void opening() {
    animator?.SetTrigger("open");
  }

  public void closing() {
    animator?.SetTrigger("close");
  }
}
