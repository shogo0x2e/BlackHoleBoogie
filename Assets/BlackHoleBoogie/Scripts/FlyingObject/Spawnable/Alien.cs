using UnityEngine;

namespace Object.Spawnable {
    public class Alien : AbstractObject {
        private const float laserRange = 100F;
        private LineRenderer laserLineRenderer;
        private bool showLaser = true;

        [SerializeField] private GameObject fireBall;

        public override void OnSpawn() {
            laserLineRenderer = GetComponent<LineRenderer>();

            transform.LookAt(BlackHole.GetInstance().GetPosition());
        }

        public new void Update() {
            if (!showLaser) {
                return;
            }

            Transform laserOrigin = transform;

            laserLineRenderer.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = laserOrigin.position;

            Vector3 laserDirection = laserOrigin.forward;
            if (Physics.Raycast(rayOrigin, laserDirection,
                    out RaycastHit raycastHit, laserRange)) {
                laserLineRenderer.SetPosition(1, raycastHit.point);
            } else {
                laserLineRenderer.SetPosition(1, rayOrigin + laserDirection * laserRange);
            }
        }

        private void Shoot() {
            showLaser = false;
        }

        public override void OnHeadCollision(Vector3 colPosition, float colForce) { }

        public override void OnSlap(Vector3 colPosition, float colForce) { }

        public override bool IsGrabbable() {
            return false;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) { }
    }
}
