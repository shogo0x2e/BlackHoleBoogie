using System;
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

            //If we implement lives again this is where we should do it
            //LifeManager.lifeCount--;

            Destroy(gameObject, 0.2F);

            sucked = true;
        }

        public void OnDestroy()
        {
            if (mainMode)
            {
                Spawner.GetInstance().OnObjectGettingSucked();
            }
        }
        

        public void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.GetComponent<AbstractObject>() != null) {
                return;
            }

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(6F, collision.contacts[0].point, 10);
            if(TutorialManager.tutorialStep == 1)
            {
                TutorialManager.tutorialStringText = "";
                TutorialManager.tutorialStep = 2;
            }

            Destroy(gameObject, 6F);
        }

        public void SetMoveSpeed(float value) {
            moveSpeed = value;
        }
    }
}
