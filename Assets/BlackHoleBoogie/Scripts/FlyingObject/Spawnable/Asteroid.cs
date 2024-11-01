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

        public override void OnHeadCollision(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softestForce) {
                return;
            }

            // Decrease score because it hurts to collide an Asteroid with your head :)
            // ScoreManager.scoreCount -= 20;
            ScoreManager.scoreCount -= 0;
            SetDestroyed(true);
        }

        public override void OnArrowCollision(Vector3 colPosition, float colForce) {
            if (IsDestroyed()) {
                return;
            }
            
            Explode(colPosition, colForce);
            ScoreManager.scoreCount += 100;
            SetDestroyed(true);
        }

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softForce) {
                return;
            }

            ScoreManager.scoreCount += 60;
            Destroy(gameObject, 6F);
            SetDestroyed(true);
        }

        public override bool IsGrabbable() {
            return false;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            if (IsDestroyed() || colForce < softForce) {
                return;
            }

            Explode(colPosition, colForce);
            ScoreManager.scoreCount += 200;
            SetDestroyed(true);

            if (TutorialManager.tutorialStep == 0) {
                TutorialManager.tutorialStringText = "Quest: Save an Astronaut!";
                TutorialManager.tutorialStep = 1;
            }
        }

        private void Explode(Vector3 colPosition, float colForce) {
            if (broken) {
                return;
            }

            // TODO: Should add collider to childrens and make them stay in space
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
