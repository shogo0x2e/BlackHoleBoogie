using UnityEngine;

namespace Object.Spawnable {
    public class Alien : AbstractObject {
        private float floatAmplitude;
        private float floatSpeed;
        private Vector3 startPosition;

        // private const float laserRange = 100F;
        private LineRenderer laserLineRenderer;

        private bool canShoot = true;

        [SerializeField] private FireBall fireBall;

        public override void OnSpawn() {
            floatAmplitude = Random.Range(0.1F, 0.2F);
            floatSpeed = Random.Range(0.6F, 1.2F);
            startPosition = transform.position;

            laserLineRenderer = GetComponent<LineRenderer>();
        }

        public new void Update() {
            if (IsDestroyed()) {
                return;
            }

            float floatY = startPosition.y + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);
            transform.position = new Vector3(startPosition.x, floatY, startPosition.z);

            transform.LookAt(BlackHole.GetInstance().GetPosition());

            if (!canShoot) {
                return;
            }

            if (Random.Range(0F, 1F) < 0.0026F) {
                Shoot();
            }

            Transform laserOrigin = transform;

            laserLineRenderer.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = laserOrigin.position;

            Vector3 laserDirection = laserOrigin.forward;
            float laserRange = Vector3.Distance(laserOrigin.position, BlackHole.GetInstance().GetPosition());

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

        public override void OnArrowCollision(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed()) {
                return;
            }

            ScoreManager.scoreCount += 300;
            laserLineRenderer.enabled = false;
            Destroy(gameObject, 3.6F);
            SetDestroyed(true);
        }

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
