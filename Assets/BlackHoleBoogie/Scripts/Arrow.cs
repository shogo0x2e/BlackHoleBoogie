using UnityEngine;

public class Arrow : MonoBehaviour {
    private const float moveSpeed = 4.8F;

    public void Start() {
        Destroy(gameObject, 10F);
    }

    public void Update() {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
        transform.Rotate(0, 0, 420F * Time.deltaTime);
    }
}
