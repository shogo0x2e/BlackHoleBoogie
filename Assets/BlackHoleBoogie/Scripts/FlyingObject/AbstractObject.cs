using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Object {
    public abstract class AbstractObject : MonoBehaviour {
        public const float softestForce = 0.2F;
        public const float softForce = 1.36F;
        public const float midForce = 2F;
        public const float hardForce = 3.0F;
        public const float hardestForce = 4.2F;

        private const float defaultRotationSpeed = 42F;
        private const float blackHoleRadius = 0.6F;

        private bool mainMode;

        [SerializeField] private float minMoveSpeed;
        [SerializeField] private float maxMoveSpeed;
        private float moveSpeed;
        private Vector3 targetPosition;

        [SerializeField] private float minRotSpeed;
        [SerializeField] private float maxRotSpeed;
        private float rotSpeed;
        private Vector3 rotDirection;

        [SerializeField] private AudioSource onDestroySound;
        private bool destroyed = false;

        private bool suckedIn = false;

        private float baseScale;
        protected const float shrinkTime = 0.6F;
        protected float shrinkTimeAcc = 0;

        private bool isGrabbed = false;

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

            baseScale = transform.localScale.x;

            OnSpawn();
        }

        public virtual void OnSpawn() { }

        public void Update() {
            if (BlackHole.paused) return; // TODO: Remove when menu is implemented

            // Destroy space objects within BH
            if (mainMode && Vector3.Distance(transform.position, targetPosition) < blackHoleRadius) {
                shrinkTimeAcc += Time.deltaTime;
                float shrinkScale = baseScale * (shrinkTime - shrinkTimeAcc) * (1F / shrinkTime);
                transform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
                GetSucked();
            }

            if (isGrabbed) {
                return;
            }

            if (moveSpeed == 0) {
                return;
            }

            float moveDelta = moveSpeed * Time.deltaTime;
            Vector3 forwardVector = (targetPosition - transform.position).normalized;
            transform.position += moveDelta * forwardVector;

            float rotDelta = rotSpeed * moveDelta; // Rotation speed depends on movement speed
            transform.Rotate(rotDelta * rotDirection.x,
                rotDelta * rotDirection.y,
                rotDelta * rotDirection.z);
        }

        public virtual void GetSucked() {
            if (suckedIn) {
                return;
            }

            if (onDestroySound != null) {
                AudioManager.PlayAudioSource(onDestroySound, transform);
            }

            // ScoreManager.scoreCount -= 20;
            ScoreManager.scoreCount -= 0;

            Destroy(gameObject, shrinkTime);

            suckedIn = true;
        }

        public void OnDestroy() {
            if (mainMode) {
                Spawner.GetInstance().OnObjectGettingSucked();
            }
        }

        public void OnCollisionEnter(Collision collision) {
            if (suckedIn) {
                return;
            }

            if (collision.gameObject.GetComponent<AbstractObject>() != null) {
                return;
            }

            GameObject colObject = collision.gameObject;
            Transform colObjectParent = colObject.transform.parent;
            ContactPoint contact = collision.contacts[0];
            Vector3 colPosition = contact.point;

            // Collision with the head
            if (colObjectParent != null && colObjectParent.CompareTag("MainCamera")) {
                float colForce = 12 * Head.GetInstance().GetVelocity() + 2;
                OnHeadCollision(colPosition, colForce);
                return;
            }

            if (colObject.gameObject.CompareTag("Arrow")) {
                OnArrowCollision(colPosition, 8F);
                return;
            }

            /*
             * Since collisions happen with fingers and not whole hands, and that
             * fingers are created at runtime (we can't assign tag to them), we
             * check for the parent of the fingers which represents the whole hand.
             */
            HandData handData = null;
            while (colObjectParent != null) {
                if (colObjectParent.CompareTag("LeftHandTag")) {
                    handData = HandsManager.GetInstance().GetLeftHandData();
                    break;
                }

                if (colObjectParent.CompareTag("RightHandTag")) {
                    handData = HandsManager.GetInstance().GetRightHandData();
                    break;
                }

                colObjectParent = colObjectParent.parent;
            }

            OnHandCollision(handData, colPosition);
        }

        public abstract void OnHeadCollision(Vector3 colPosition, float colForce);

        public abstract void OnArrowCollision(Vector3 colPosition, float colForce);

        private void OnHandCollision(HandData handData, Vector3 colPosition) {
            float colForce = handData.GetVelocity();

            switch (handData.GetHandShape()) {
                case HandData.HandShape.Other:
                    break;
                case HandData.HandShape.Open:
                    OnSlap(colPosition, colForce);
                    break;
                case HandData.HandShape.Grab:
                    OnGrab(handData);
                    break;
                case HandData.HandShape.Rock:
                    OnPunch(colPosition, colForce);
                    break;
                default:
                    break;
            }
        }

        public abstract void OnSlap(Vector3 colPosition, float colForce);

        private void OnGrab(HandData handData) {
            if (!IsGrabbable()) {
                return;
            }

            handData.GrabObject(this);
        }

        public abstract bool IsGrabbable();

        public abstract void OnPunch(Vector3 colPosition, float colForce);

        public void KnockBack(Vector3 srcPosition, float srcForce) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(srcForce, srcPosition, 10F);
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }

        public void SetDestroyed(bool value) {
            destroyed = value;
        }

        public bool IsDestroyed() {
            return destroyed;
        }

        public bool IsSuckedIn() {
            return suckedIn;
        }

        public void SetIsGrabbed(bool value) {
            isGrabbed = value;
        }
    }
}
