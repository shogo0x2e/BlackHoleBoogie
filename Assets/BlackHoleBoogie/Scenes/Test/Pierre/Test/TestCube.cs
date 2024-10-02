using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {

    public void Start() {
        Invoke(nameof(ApplyForceToRigidbody), 1F);
    }

    public void ApplyForceToRigidbody() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 1000);
    }

    public void OnPunch() {
        Debug.Log("Punch Detected !");
    }
}
