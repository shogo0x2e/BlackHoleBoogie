using Object.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Object.Spawnable {
    public class Asteroid : AbstractObject {
        [SerializeField] private AsteroidManager asteroidManager;

        private GameObject currentModel;

        [SerializeField] private AudioSource explAudioSource;
        [SerializeField] private GameObject explParticles;
        private const float explForceFactor = 80F;
        private bool broken = false;

        public override void OnSpawn() {
            GameObject[] asteroidModels = asteroidManager.GetAsteroidModels();
            GameObject selectedModel = asteroidModels[Random.Range(0, asteroidModels.Length)];
            currentModel = Instantiate(selectedModel, transform.position, transform.rotation);
            currentModel.transform.parent = transform;
        }

        public new void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.GetComponent<AbstractObject>() != null) {
                return; // Do not explode when colliding with other space objects
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 colPosition = contact.point;
            Vector3 colVelocity = collision.relativeVelocity;
            float colForce = colVelocity.magnitude * collision.rigidbody.mass;

            Explode(colPosition, colForce);
        }

        private void Explode(Vector3 colPosition, float colForce) {
            if (broken) {
                return;
            }

            ScoreManager.scoreCount++;

            Transform[] trsfs = currentModel.GetComponentsInChildren<Transform>();
            foreach (Transform trsf in trsfs) {
                if (trsf.GetComponent<Rigidbody>() == null) {
                    trsf.gameObject.AddComponent<Rigidbody>();
                }
            }

            Rigidbody[] rbs = currentModel.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs) {
                rb.AddExplosionForce(colForce * explForceFactor, colPosition, 10);
            }

            AudioManager.PlayAudioSource(explAudioSource, transform);
            ParticleManager.PlayParticle(explParticles, transform.position);

            broken = true;
            SetMoveSpeed(0F); // TODO: Temporary Fix

            Destroy(gameObject, 2F);
        }
    }
}
