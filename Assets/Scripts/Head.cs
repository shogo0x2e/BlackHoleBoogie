using UnityEngine;

public class Head : MonoBehaviour {
    private static Head instance;

    public static Head GetInstance() {
        return instance;
    }

    private Vector3 previousPosition = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero;

    public void Start() {
        instance = this;
    }

    public void Update() {
        Vector3 deltaPosition = transform.position - previousPosition;
        currentVelocity = deltaPosition / Time.deltaTime;
        previousPosition = transform.position;
    }
    
    public float GetVelocity() {
        return currentVelocity.magnitude;
    }
}
