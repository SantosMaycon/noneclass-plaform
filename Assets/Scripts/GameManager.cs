using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField] private static int lifeOfPlayer = 3;
  [SerializeField] private Image[] hearts;
  // Start is called before the first frame update
  void Start() {
    printHeartsOnScreen();
  }

  // Update is called once per frame
  void Update() {}

  public int getLifeOfPlayer() {
    return lifeOfPlayer;
  }

  public void setLifeOfPlayer(int life) {
    lifeOfPlayer = life;
  }

  public void printHeartsOnScreen() {
    var index = 0;
    foreach (var heart in hearts) {
      heart.enabled = index < lifeOfPlayer ? true : false;
      index++;
    }
  }

  public void resetGame() {
    lifeOfPlayer = 3;
    changeScene("Scene 1");
  }
  public void changeScene(string scene) {
    SceneManager.LoadScene(scene);
  }
}
