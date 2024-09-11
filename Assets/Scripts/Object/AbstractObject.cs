using UnityEngine;
using Utils;

namespace Object {
    public abstract class AbstractObject : MonoBehaviour {
        public float minMoveSpeed;
        public float maxMoveSpeed;
        public float minRotSpeed;
        public float maxRotSpeed;

        private float moveSpeed;
        private Vector3 targetPosition;
        private float rotSpeed;
        private Vector3 rotDirection;

        public void Start() {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            targetPosition = Vector3.zero;

            rotSpeed = maxRotSpeed != 0
                ? Random.Range(minRotSpeed, maxRotSpeed)
                : 42F;
            rotDirection = Vector.GetRandomDirection();

            OnSpawn();
        }

        protected virtual void OnSpawn() { }

        public void Update() {
            float moveDelta = moveSpeed * Time.deltaTime;
            Vector3 forwardVector = (targetPosition - transform.position).normalized;
            transform.position += moveDelta * forwardVector;

            float rotDelta = rotSpeed * moveDelta; // Rotation speed depends on movement speed
            transform.Rotate(rotDelta * rotDirection.x,
                rotDelta * rotDirection.y,
                rotDelta * rotDirection.z);
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }
    }
}
