using Object;
using UnityEngine;

public class HandData : MonoBehaviour {
    private Vector3 previousPosition = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero;

    public enum HandShape {
        Other,
        Open,
        Grab,
        Rock
    }

    private HandShape handShape = HandShape.Open;

    private AbstractObject grabbedObject = null;

    [SerializeField] private Material handMaterial;

    public void Update() {
        Vector3 deltaPosition = transform.position - previousPosition;
        currentVelocity = deltaPosition / Time.deltaTime;
        previousPosition = transform.position;
    }

    public void GrabObject(AbstractObject spaceObject) {
        if (grabbedObject != null) {
            return;
        }

        grabbedObject = spaceObject;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.transform.SetParent(transform);
        grabbedObject.SetIsGrabbed(true);
    }

    private void ReleaseObject() {
        if (grabbedObject == null) {
            return;
        }

        grabbedObject.SetIsGrabbed(false);
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
    }

    public float GetVelocity() {
        return currentVelocity.magnitude;
    }

    public void SetHandShape(HandShape value) {
        if (value != HandShape.Grab) {
            ReleaseObject();
        }

        handShape = value;
    }

    public HandShape GetHandShape() {
        return handShape;
    }

    public void SetHandMaterialColor(Color color) {
        handMaterial.color = color;
    }
}
