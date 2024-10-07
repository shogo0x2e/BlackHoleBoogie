using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HandTipLaser : MonoBehaviour {
    private const float laserRange = 100F;

    private HandData handData;

    private LineRenderer laserLineRenderer;

    public void Start() {
        laserLineRenderer = GetComponent<LineRenderer>();
    }

    public void Update() {
        Transform laserOrigin = handData.GetHandIndexTip().transform;

        laserLineRenderer.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = laserOrigin.position;

        Vector3 laserDirection = laserOrigin.right;
        if (handData.GetHandType() == HandData.HandType.Left) {
            laserDirection = -laserDirection;
        }

        if (Physics.Raycast(rayOrigin, laserDirection,
                out RaycastHit raycastHit, laserRange)) {
            laserLineRenderer.SetPosition(1, raycastHit.point);
        } else {
            laserLineRenderer.SetPosition(1, rayOrigin + laserDirection * laserRange);
        }
    }

    public void SetHandData(HandData value) {
        handData = value;
    }
}
