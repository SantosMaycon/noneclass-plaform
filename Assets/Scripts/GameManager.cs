using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField] private static int lifeOfPlayer = 3;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  public int getLifeOfPlayer() {
    return lifeOfPlayer;
  }

  public void setLifeOfPlayer(int life) {
    lifeOfPlayer = life;
  }
  public void changeScene(string scene) {
    SceneManager.LoadScene(scene);
  }
}
