using Object.Spawnable;
using UnityEngine;

public class FireBall : MonoBehaviour {
    private Alien srcShooter;

    public void Start() {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 direction = (BlackHole.GetInstance().GetPosition() - transform.position).normalized;
        rb.AddForce(6F * direction, ForceMode.Impulse);

        Destroy(gameObject, 6F);
    }

    public void OnDestroy() {
        srcShooter.SetCanShoot(true);
    }

    public void SetShooter(Alien value) {
        srcShooter = value;
    }
}
