using UnityEngine;

public class Head : MonoBehaviour {
    private static Head instance;

    public static Head GetInstance() {
        return instance;
    }

    public void Start() {
        instance = this;
    }
}
