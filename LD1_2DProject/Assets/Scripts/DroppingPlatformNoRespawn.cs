﻿using UnityEngine;
using System.Collections;

public class DroppingPlatformNoRespawn : MonoBehaviour
{
    Rigidbody rb;
    public GameObject particleFX;
    private Vector3 originalPosition;
    public float resetPositionAfterSeconds = 10f;

    private AudioSource auds;
    public AudioClip fallingSound;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalPosition = this.gameObject.transform.position;
        originalPosition.y = originalPosition.y - 15;
        auds = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rb.useGravity = true;
            particleFX.SetActive(true);
            //Make falling sound
            auds.PlayOneShot(fallingSound);
            StartCoroutine(Delay(resetPositionAfterSeconds));
        }
    }

    void ResetPlatform()
    {
        //Turn Gravity Off
        rb.useGravity = false;
        //Move Platform back to original position
        this.gameObject.transform.position = originalPosition;
        this.gameObject.transform.rotation = Quaternion.identity;
        //Turn off "On" Particle Effect
        particleFX.SetActive(false);
    }

    IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetPlatform();
    }
}
