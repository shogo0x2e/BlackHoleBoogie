using Object;
using System;
using UnityEngine;

public class HandData : MonoBehaviour {
    private Vector3 previousPosition = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero;

    public enum HandShape {
        Other,
        Open,
        Grab,
        Rock,
        Gun
    }

    private HandShape handShape = HandShape.Open;

    private AbstractObject grabbedObject = null;

    [SerializeField] private Material handMaterial;

    private Transform handIndexTip;
    private Vector3 previousPositionTip = Vector3.zero;
    private Vector3 currentVelocityTip = Vector3.zero;

    public void Start() {
        handIndexTip = FindIndex(transform);
    }

    private static Transform FindIndex(Transform parNode) {
        foreach (Transform childNode in parNode) {
            if (childNode.name == "Hand_IndexTip") {
                return childNode;
            }

            if (childNode.childCount != 0) {
                Transform result = FindIndex(childNode);
                if (result != null) {
                    return result;
                }
            }
        }

        return null;
    }

    public void Update() {
        Vector3 deltaPosition = transform.position - previousPosition;
        currentVelocity = deltaPosition / Time.deltaTime;
        previousPosition = transform.position;

        Vector3 deltaPositionTip = transform.position - previousPositionTip;
        currentVelocityTip = deltaPositionTip / Time.deltaTime;
        previousPositionTip = transform.position;

        if (handShape == HandShape.Gun) {
            if (GetVelocity() < 11111) { // TODO:
                if (GetVelocityTip() > 1111) { }
            }
        }
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

    public float GetVelocityTip() {
        return currentVelocityTip.magnitude;
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
