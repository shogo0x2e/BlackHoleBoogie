using UnityEngine;

public class HandData : MonoBehaviour {
    public enum HandShape {
        Open,
        Rock
    }

    private HandShape handShape = HandShape.Open;

    [SerializeField] private Material handMaterial;

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
