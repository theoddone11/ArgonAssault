using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour

{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves in x or y axes")]
    [SerializeField] float xSpeed = 15f;
    [SerializeField] float ySpeed = 15f;
    [SerializeField] float xRange = 12f;
    [SerializeField] float yRange = 5f;
    [Header("Flying Settings")]
    [Tooltip("How fast ship rotates up and down")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
      [Tooltip("How fast ship rotates left and right")]
    [SerializeField] float rollPitchFactor = 20f;
         [Tooltip("How fast ship turns left and right")]
    [SerializeField] float yawFactor = 2;
    [SerializeField] GameObject[] lasers;
    ParticleSystem laserz;
    float xThrow, yThrow;


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    
    void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow + controlPitchFactor;
        float rollOnScreenPosition = transform.localPosition.x * 2;
        float roll = xThrow * rollPitchFactor + rollOnScreenPosition;
        float pitch = 2 * (pitchDueToPosition + pitchDueToControlThrow + 10);
        float yaw = transform.localPosition.x * positionPitchFactor * yawFactor;
       
       // float yaw = 0f;
       // float roll = 0f;
        transform.localRotation = Quaternion.Euler(pitch, -yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * xSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * ySpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
    
        transform.localPosition = new Vector3(clampedXPos,
        clampedYPos,
        transform.localPosition.z);
        
    }
    void ProcessFiring() {

        if(Input.GetButton("Fire1")){
           // ActiveLasers();
           SetLasersActive(true);
            
        }
        else{
           // DeactivateLasers();
            SetLasersActive(false);
        }
    }
 

    void SetLasersActive(bool isactive)
    {

        foreach(GameObject laser in lasers) {
            laser.SetActive(isactive);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isactive;
        }
    }

}
