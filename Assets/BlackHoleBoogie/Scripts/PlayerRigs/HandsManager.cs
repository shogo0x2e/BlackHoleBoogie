using System.Collections;
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
    private readonly Color gunColor = Color.cyan;
    private readonly Color indexColor = Color.white;

    [SerializeField] private HandTipLaser leftHandTipLaser;
    [SerializeField] private HandTipLaser rightHandTipLaser;

    public void Start() {
        instance = this;

        leftHandData.SetHandType(HandData.HandType.Left);
        rightHandData.SetHandType(HandData.HandType.Right);

        leftHandTipLaser.SetHandData(leftHandData);
        rightHandTipLaser.SetHandData(rightHandData);
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
        if (TimeManager.secondsLeft > 0) BlackHole.paused = false; // TODO: Remove when menu is implemented

        rightHandData.SetHandShape(HandData.HandShape.Rock);
        rightHandData.SetHandMaterialColor(rockColor);
    }

    public void OnLeftGunShape() {
        leftHandData.SetHandShape(HandData.HandShape.Gun);
        leftHandData.SetHandMaterialColor(gunColor);
    }

    public void OnRightGunShape() {
        rightHandData.SetHandShape(HandData.HandShape.Gun);
        rightHandData.SetHandMaterialColor(gunColor);
    }

    public void OnLeftIndexShape() {
        if (leftHandData.GetHandShape() == HandData.HandShape.Gun) {
            leftHandData.SetHandShape(HandData.HandShape.Index);
            leftHandData.SetHandMaterialColor(indexColor);
        }
    }

    public void OnRightIndexShape() {
        if (rightHandData.GetHandShape() == HandData.HandShape.Gun) {
            rightHandData.SetHandShape(HandData.HandShape.Index);
            rightHandData.SetHandMaterialColor(indexColor);
        }
    }

    public HandData GetLeftHandData() {
        return leftHandData;
    }

    public HandData GetRightHandData() {
        return rightHandData;
    }
}
