using UnityEngine;

public class HandsManager : MonoBehaviour {
    private readonly Color openColor = Color.white;
    private readonly Color rockColor = Color.red;

    [SerializeField] private Material leftHandMaterial;
    [SerializeField] private Material rightHandMaterial;

    public void OnLeftOpenShape() {
        leftHandMaterial.color = openColor;
    }
    
    public void OnRightOpenShape() {
        rightHandMaterial.color = openColor;
    }
    
    public void OnLeftRockShape() {
        leftHandMaterial.color = rockColor;
    }

    public void OnRightRockShape() {
        rightHandMaterial.color = rockColor;
    }
}
