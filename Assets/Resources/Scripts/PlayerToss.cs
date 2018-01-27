﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToss : MonoBehaviour {
    public float force, height, side;
    private Rigidbody rb;
    public GameObject playerControll;
    public GameObject cam;
    public GameManager gm;
    bool gameOver = false;
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(side, height, force));
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Top Trigger" && !gameOver)
        {
            playerControll.SetActive(true);
            playerControll.transform.position = other.transform.parent.transform.position;
            playerControll.GetComponent<PlayerControll>().car = other.transform.parent.gameObject;
            Destroy(other.transform.parent.GetComponent<Rigidbody>());
            other.transform.parent.parent = playerControll.transform; //set car as child of Player
            cam.GetComponent<CameraFollow>().target = playerControll;
            gm.RemoveCarFromSegment(other.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            gameOver= true;
            //Debug.Log("HIT ROAD");
            gm.SetSpeedScenery(0);
            gm.SetSpeedCars(-10);
        }

    }
}
