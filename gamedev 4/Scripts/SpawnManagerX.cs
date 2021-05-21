using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRangeX = 10;
    private float spawnZMin = 15;
    private float spawnZMax = 25;
    public int enemyCount;
    public int waveCount = 1;
    public GameObject player; 

    void Update() {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }

    }

    //random position for powerups and balls
    Vector3 GenerateSpawnPosition () {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn) {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);
        //add powerup if there is fuck all left
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }
        //spawn balls for wave
        for (int i = 0; i < enemiesToSpawn; i++) {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            EnemyX ec = enemy.GetComponent<EnemyX>();
            ec.speed += waveCount * 10;
        }
        waveCount++;
        ResetPlayerPosition();

    }

    //reset player
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
