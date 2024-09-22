using Object.Manager;
using UnityEngine;
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

        public override void OnSlap(Vector3 colPosition, float colForce) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(colForce, colPosition, 10F);
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            Explode(colPosition, colForce);
        }

        private void Explode(Vector3 colPosition, float colForce) {
            if (broken) {
                return;
            }

            ScoreManager.scoreCount++;

            Transform[] trsfs = currentModel.GetComponentsInChildren<Transform>();
            foreach (Transform trsf in trsfs) {
                if (trsf.GetComponent<Rigidbody>() != null) {
                    return;
                }

                Rigidbody rb = trsf.gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
            }

            Rigidbody[] rbs = currentModel.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs) {
                rb.AddExplosionForce(colForce * explForceFactor, colPosition, 10F);
            }

            AudioManager.PlayAudioSource(explAudioSource, transform);
            ParticleManager.PlayParticle(explParticles, transform.position);

            broken = true;
            SetMoveSpeed(0F); // TODO: Temporary Fix

            Destroy(gameObject, 2F);
        }
    }
}
