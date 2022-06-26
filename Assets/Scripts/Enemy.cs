using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject hitMarker;
    [SerializeField] Transform parent;
    [SerializeField] int enemyScoreValue = 1;
    [SerializeField] int enemyHitPoints = 3;
    Rigidbody rb;
    int enemyHealth = 3;
    
    Scoreboard scoreBoard;

    void Start(){
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        enemyHealth = enemyHitPoints;
        scoreBoard = FindObjectOfType<Scoreboard>();
    }
    void OnParticleCollision(GameObject other) {
        ProcessHit();
        if (enemyHealth == 0){
        KillEnemy();
        }
 
        

    }
    void ProcessHit(){
        if(enemyHealth > 0){
        enemyHealth -= 1;
        }
        GameObject hitInstance = Instantiate(hitMarker, transform.position, Quaternion.identity);
        hitInstance.transform.parent = parent;
        scoreBoard.IncreaseScore(enemyScoreValue);
    }
    void KillEnemy(){
  
        GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
       
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
