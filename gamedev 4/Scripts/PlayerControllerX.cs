using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour {
    private Rigidbody playerRb;
    private float speed = 525;
    private float boostPower = 1.1;
    private GameObject focalPoint;
    private ParticleSystem particleSystem;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 8;
    private float normalStrength = 11;
    private float powerupStrength = 27;
    
    void Start() {
        playerRb = GetComponent<Rigidbody>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update() {
        float verticalInput = Input.GetAxis("Vertical");
        bool boostInput = Input.GetButton("Boost");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        if (boostInput)
        {
            playerRb.AddForce(focalPoint.transform.forward * boostPower, ForceMode.Impulse);
            particleSystem.Play();
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    //powerup pickujp
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Powerup")) {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    IEnumerator PowerupCooldown() {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    //player absolutely BONKS the shit out of something
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position;
            if (hasPowerup) {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            } else 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
