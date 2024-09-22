using UnityEngine;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }

        public override void OnGrab(Vector3 colPosition, float colForce) { }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }
    }
}
