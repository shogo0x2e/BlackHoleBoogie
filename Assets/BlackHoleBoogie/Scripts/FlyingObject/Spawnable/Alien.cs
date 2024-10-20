﻿using UnityEngine;

namespace Object.Spawnable {
    public class Alien : AbstractObject {
        private const float laserRange = 100F;
        private LineRenderer laserLineRenderer;

        private bool canShoot = true;

        [SerializeField] private FireBall fireBall;

        public override void OnSpawn() {
            laserLineRenderer = GetComponent<LineRenderer>();

            transform.LookAt(BlackHole.GetInstance().GetPosition());
        }

        public new void Update() {
            if (!canShoot) {
                return;
            }

            if (Random.Range(0F, 1F) < 0.002F) {
                Shoot();
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
            SetCanShoot(false);

            FireBall fireBallInstance = Instantiate(fireBall,
                transform.position + 1.2F * transform.forward,
                Quaternion.identity);
            fireBallInstance.SetShooter(this);
        }

        public override void OnHeadCollision(Vector3 colPosition, float colForce) { }

        public override void OnSlap(Vector3 colPosition, float colForce) { }

        public override bool IsGrabbable() {
            return false;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) { }

        public void SetCanShoot(bool value) {
            canShoot = value;
            laserLineRenderer.enabled = canShoot;
        }
    }
}
