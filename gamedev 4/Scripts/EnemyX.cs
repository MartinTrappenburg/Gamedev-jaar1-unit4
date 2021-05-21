using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour {
    public float speed;
    private GameObject playerGoal;
    private Rigidbody enemyRb;

    void Start() {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
    }
    void Update()
    {
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        //yeet enemy if it hits a goal
        if (other.gameObject.name == "Enemy Goal") {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal") {
            Destroy(gameObject);
        }
    }
}
