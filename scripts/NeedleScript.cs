using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour {

    public Rigidbody needle;
    private Vector3 velocity;
    public float rotSpeed = 15;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0, rotSpeed, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Quaternion deltaRotation = Quaternion.Euler(velocity * Time.deltaTime);
        needle.MoveRotation(needle.rotation * deltaRotation);
    }
}
