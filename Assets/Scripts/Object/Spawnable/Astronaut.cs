﻿using UnityEngine;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
        public override void OnHeadCollision(Vector3 colPosition) {
            KnockBack(colPosition, 10F); // TODO: Fix colForce
        }

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }

        public override bool IsGrabbable() {
            return true;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }
    }
}
