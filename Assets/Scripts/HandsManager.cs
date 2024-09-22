using UnityEngine;

public class HandsManager : MonoBehaviour {
    private static HandsManager instance;

    public static HandsManager GetInstance() {
        return instance;
    }

    [SerializeField] private HandData leftHandData;
    [SerializeField] private HandData rightHandData;

    private readonly Color openColor = Color.white;
    private readonly Color rockColor = Color.red;

    public void Start() {
        instance = this;
    }

    public void OnLeftOpenShape() {
        leftHandData.SetHandShape(HandData.HandShape.Open);
        leftHandData.SetHandMaterialColor(openColor);
    }

    public void OnRightOpenShape() {
        rightHandData.SetHandShape(HandData.HandShape.Open);
        rightHandData.SetHandMaterialColor(openColor);
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
