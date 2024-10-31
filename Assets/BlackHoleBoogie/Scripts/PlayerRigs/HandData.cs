using Object;
using UnityEngine;

public class HandData : MonoBehaviour {
    private HandType handType;

    public enum HandType {
        Left,
        Right
    }

    private Vector3 previousPosition = Vector3.zero;
    private Vector3 currentVelocity = Vector3.zero;

    public enum HandShape {
        Other,
        Open,
        Grab,
        Rock,
        Gun,
        Index
    }

    private HandShape handShape = HandShape.Open;

    private AbstractObject grabbedObject = null;

    [SerializeField] private Material handMaterial;

    private Transform handIndexTip;
    private const float shootTimeCd = 1F;
    private float shootTimeAcc = 0;

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

        if (handShape == HandShape.Gun || handShape == HandShape.Index) {
            shootTimeAcc -= Time.deltaTime;
            if (shootTimeAcc < 0) {
                ShootArrow();

                shootTimeAcc = shootTimeCd;
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

    private void ShootArrow() {
        Vector3 arrowDirection = handIndexTip.transform.right;
        if (GetHandType() == HandType.Left) {
            arrowDirection = -arrowDirection;
        }

        GameObject arrowGameObject = HandsManager.GetInstance().GetArrowGameObject();
        Instantiate(
            arrowGameObject,
            handIndexTip.transform.position,
            Quaternion.LookRotation(arrowDirection));
    }

    public void SetHandType(HandType value) {
        handType = value;
    }

    public HandType GetHandType() {
        return handType;
    }

    public float GetVelocity() {
        return currentVelocity.magnitude;
    }

    public void SetHandShape(HandShape value) {
        if (value != HandShape.Grab) {
            ReleaseObject();
        }

        if (value != HandShape.Gun && value != HandShape.Index) {
            if (handType == HandType.Left) {
                HandsManager.GetInstance().GetLeftHandTipLaser().SetShowLaser(false);
            } else {
                HandsManager.GetInstance().GetRightHandTipLaser().SetShowLaser(false);
            }
        } else {
            // shootTimeAcc = 0;
        }

        // if (handShape == HandShape.Gun && value == HandShape.Index) {
        //     ShootArrow();
        // }

        handShape = value;
    }

    public HandShape GetHandShape() {
        return handShape;
    }

    public void SetHandMaterialColor(Color color) {
        handMaterial.color = color;
    }

    public Transform GetHandIndexTip() {
        return handIndexTip;
    }
}
