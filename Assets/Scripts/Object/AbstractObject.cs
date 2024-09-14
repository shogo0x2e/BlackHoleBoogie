using UnityEngine;
using Utils;

namespace Object {
    public abstract class AbstractObject : MonoBehaviour {
        private const float defaultRotationSpeed = 42F;
        private const double blackHoleRadius = 1;

        [SerializeField] private float minMoveSpeed;
        [SerializeField] private float maxMoveSpeed;
        private float moveSpeed;
        private Vector3 targetPosition;

        [SerializeField] private float minRotSpeed;
        [SerializeField] private float maxRotSpeed;
        private float rotSpeed;
        private Vector3 rotDirection;

        [SerializeField] private AudioSource onDestroySound;

        public void Start() {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            targetPosition = BlackHole.GetInstance().GetPosition();

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

            // Destroy object within BH
            if (transform.position.magnitude < blackHoleRadius) {
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
            AudioSource onDestroySoundInstance = Instantiate(onDestroySound, transform.position, transform.rotation);
            onDestroySoundInstance.Play();

            Spawner.GetInstance().OnObjectDestroyed();

            LifeManager.lifeCount--;
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }
    }
}
