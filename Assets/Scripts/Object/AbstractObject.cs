using UnityEngine;
using Utils;

namespace Object {
    public abstract class AbstractObject : MonoBehaviour {
        private const float defaultRotationSpeed = 42F;
        private const double BH_radius = 4;

        [SerializeField] private float minMoveSpeed;
        [SerializeField] private float maxMoveSpeed;
        [SerializeField] private float minRotSpeed;
        [SerializeField] private float maxRotSpeed;
        [SerializeField] private AudioSource onDestroySound;
        
        private float moveSpeed;
        private Vector3 targetPosition;
        private float rotSpeed;
        private Vector3 rotDirection;

        public void Start() {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            targetPosition = Vector3.zero;

            rotSpeed = maxRotSpeed != 0
                ? Random.Range(minRotSpeed, maxRotSpeed)
                : defaultRotationSpeed;
            rotDirection = Vector.GetRandomDirection();
            OnSpawn();
        }

        public virtual void OnSpawn() { }

        public void Update() {
            float moveDelta = moveSpeed * Time.deltaTime;
            moveSpeed = moveSpeed + 0.01F; // A bit of acceleration
            Vector3 forwardVector = (targetPosition - transform.position).normalized;
            transform.position += moveDelta * forwardVector;
            // Destroy object within BH
            if (transform.position.magnitude < BH_radius) {
                float shrinkScale = 1 - 100 * Time.deltaTime;
                transform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
                Destroy(gameObject, 0.2F);
                return;
            }

            float rotDelta = rotSpeed * moveDelta; // Rotation speed depends on movement speed
            transform.Rotate(rotDelta * rotDirection.x,
                rotDelta * rotDirection.y,
                rotDelta * rotDirection.z);
        }

        public void OnDestroy() {
            LifeManager.lifeCount -= 1;
            AudioSource onDestroySoundInstance = Instantiate(onDestroySound, transform.position, transform.rotation);
            onDestroySoundInstance.Play();
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }
    }
}
