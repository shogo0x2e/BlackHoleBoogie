using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Object {
    public abstract class AbstractObject : MonoBehaviour {
        private const float defaultRotationSpeed = 42F;
        private const float blackHoleRadius = 0.8F;

        private bool mainMode;

        [SerializeField] private float minMoveSpeed;
        [SerializeField] private float maxMoveSpeed;
        private float moveSpeed;
        private Vector3 targetPosition;

        [SerializeField] private float minRotSpeed;
        [SerializeField] private float maxRotSpeed;
        private float rotSpeed;
        private Vector3 rotDirection;

        private bool sucked = false;

        [SerializeField] private AudioSource onDestroySound;

        public void Start() {
            mainMode = BlackHole.GetInstance() != null;

            targetPosition = mainMode
                ? BlackHole.GetInstance().GetPosition()
                : Vector3.zero;

            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

            rotSpeed = maxRotSpeed != 0
                ? Random.Range(minRotSpeed, maxRotSpeed)
                : defaultRotationSpeed;
            rotDirection = Vector.GetRandomDirection();
            OnSpawn();
        }

        public virtual void OnSpawn() { }

        public void Update() {
            float moveDelta = moveSpeed * Time.deltaTime;
            Vector3 forwardVector = (targetPosition - transform.position).normalized;
            transform.position += moveDelta * forwardVector;

            // Destroy space objects within BH
            if (mainMode && Vector3.Distance(transform.position, targetPosition) < blackHoleRadius) {
                float shrinkScale = 1 - 100 * Time.deltaTime;
                transform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
                GetSucked();
                return;
            }

            float rotDelta = rotSpeed * moveDelta; // Rotation speed depends on movement speed
            transform.Rotate(rotDelta * rotDirection.x,
                rotDelta * rotDirection.y,
                rotDelta * rotDirection.z);
        }

        private void GetSucked() {
            if (sucked) {
                return;
            }

            AudioManager.PlayAudioSource(onDestroySound, transform);

            LifeManager.lifeCount--;

            Destroy(gameObject, 0.2F);

            sucked = true;
        }

        public void OnDestroy() {
            if (mainMode) {
                Spawner.GetInstance().OnObjectGettingSucked();
            }
        }

        public void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.GetComponent<AbstractObject>() != null) {
                return;
            }

            GameObject colObject = collision.gameObject;

            /*
             * Since collisions happen with fingers and not whole hands, and that
             * fingers are created at runtime (we can't assign tag to them), we
             * check for the parent of the fingers which should eventually arrive
             * to the actual hand containing said fingers.
             */
            Transform parent = colObject.transform;
            HandData handData = null;
            while (parent != null) {
                if (parent.CompareTag("LeftHandTag")) {
                    handData = HandsManager.GetInstance().GetLeftHandData();
                    break;
                }

                if (parent.CompareTag("RightHandTag")) {
                    handData = HandsManager.GetInstance().GetRightHandData();
                    break;
                }

                parent = parent.parent;
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 colPosition = contact.point;

            OnHandCollision(handData, colPosition);
        }

        private void OnHandCollision(HandData handData, Vector3 colPosition) {
            float colForce = handData.GetVelocity();

            switch (handData.GetHandShape()) {
                case HandData.HandShape.Open:
                    OnSlap(colPosition, colForce);
                    break;
                case HandData.HandShape.Grab:
                    OnGrab(colPosition, colForce);
                    break;
                case HandData.HandShape.Rock:
                    OnPunch(colPosition, colForce);
                    break;
                default:
                    Debug.Log("Hand: No Shape Detected !");
                    break;
            }
        }

        public abstract void OnSlap(Vector3 colPosition, float colForce);

        public abstract void OnGrab(Vector3 colPosition, float colForce);

        public abstract void OnPunch(Vector3 colPosition, float colForce);

        public void KnockBack(Vector3 srcPosition, float srcForce) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(srcForce, srcPosition, 10F);
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }
    }
}
