using UnityEngine;

public class HandData : MonoBehaviour {
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    public enum HandShape {
        Open,
        Grab,
        Rock
    }

    private HandShape handShape = HandShape.Open;

    [SerializeField] private Material handMaterial;

    public void Start() {
        previousPosition = transform.position;
    }

    public void Update() {
        Vector3 deltaPosition = transform.position - previousPosition;
        currentVelocity = deltaPosition / Time.deltaTime;
        previousPosition = transform.position;
    }

    public float GetVelocity() {
        return currentVelocity.magnitude;
    }

    public void SetHandShape(HandShape value) {
        handShape = value;
    }

    public HandShape GetHandShape() {
        return handShape;
    }

    public void SetHandMaterialColor(Color color) {
        handMaterial.color = color;
    }
}
