using UnityEngine;

public class BlackHole : MonoBehaviour {
    private static BlackHole instance;

    public static BlackHole GetInstance() {
        return instance;
    }

    public void Start() {
        instance = this;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}