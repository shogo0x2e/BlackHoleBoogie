using UnityEngine;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
        public override void OnSlap(Vector3 colPosition, float colForce) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(colForce, colPosition, 10F);

            Destroy(gameObject, 6F);
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(colForce, colPosition, 10F);

            Destroy(gameObject, 6F);
        }
    }
}
