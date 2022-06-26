using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] int reloadDelay = 2;
   void OnCollisionEnter(Collision other) {
       Debug.Log(this.name + " collided with " + other.gameObject.name);
       StartCrashSequence();

  }


  void StartCrashSequence(){
        explosion.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
  }
void ReloadLevel(){
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(0);
    }
    
 void OnTriggerEnter(Collider other) {
    Debug.Log(this.name + " triggered " + other.gameObject.name);
  }


}
