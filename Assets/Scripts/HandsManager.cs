using UnityEngine;

public class HandsManager : MonoBehaviour {
    private static HandsManager instance;

    public static HandsManager GetInstance() {
        return instance;
    }

    [SerializeField] private HandData leftHandData;
    [SerializeField] private HandData rightHandData;

    private readonly Color otherColor = Color.white;
    private readonly Color openColor = Color.yellow;
    private readonly Color grabColor = Color.green;
    private readonly Color rockColor = Color.red;

    public void Start() {
        instance = this;
    }

    public void OnLeftOtherShape() {
        // leftHandData.SetHandShape(HandData.HandShape.Other);
        // leftHandData.SetHandMaterialColor(otherColor);
    }

    public void OnRightOtherShape() {
        // rightHandData.SetHandShape(HandData.HandShape.Other);
        // rightHandData.SetHandMaterialColor(otherColor);
    }

    public void OnLeftOpenShape() {
        leftHandData.SetHandShape(HandData.HandShape.Open);
        leftHandData.SetHandMaterialColor(openColor);
    }

    public void OnRightOpenShape() {
        rightHandData.SetHandShape(HandData.HandShape.Open);
        rightHandData.SetHandMaterialColor(openColor);
    }

    public void OnLeftGrabShape() {
        leftHandData.SetHandShape(HandData.HandShape.Grab);
        leftHandData.SetHandMaterialColor(grabColor);
    }

    public void OnRightGrabShape() {
        rightHandData.SetHandShape(HandData.HandShape.Grab);
        rightHandData.SetHandMaterialColor(grabColor);
    }

    public void OnNotLeftGrabShape() {
        // leftHandData.ReleaseObject();
    }

    public void OnNotRightGrabShape() {
        // rightHandData.ReleaseObject();
    }

    public void OnLeftRockShape() {
        leftHandData.SetHandShape(HandData.HandShape.Rock);
        leftHandData.SetHandMaterialColor(rockColor);
    }

    public void OnRightRockShape() {
        rightHandData.SetHandShape(HandData.HandShape.Rock);
        rightHandData.SetHandMaterialColor(rockColor);
    }

    public HandData GetLeftHandData() {
        return leftHandData;
    }

    public HandData GetRightHandData() {
        return rightHandData;
    }
}
