using UnityEngine;
using Utils;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject
    {
        [SerializeField] private GameObject ragdollPrefab;
        [SerializeField] private AudioSource hitSound1;
        [SerializeField] private AudioSource hitSound2;
        private bool soundAlternator;

        public override void OnHeadCollision(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softestForce) {
                return;
            }

            ScoreManager.scoreCount += 10;
            SetDestroyed(true);
        }

        public override void OnArrowCollision(Vector3 colPosition, float colForce) {

            if (IsDestroyed()) {
                return;
            }
            
            // ScoreManager.scoreCount -= 20;
            ScoreManager.scoreCount -= 0;
            Destroy(gameObject, 3.6F);
            SetDestroyed(true);
            ReplaceToRagdoll(colPosition, colForce);
        }

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softForce) {
                return;
            }

            PlayHitSound();
            ScoreManager.scoreCount += 100;
            Destroy(gameObject, 6F);
            SetDestroyed(true);

            if (TutorialManager.tutorialStep == 1) {
                TutorialManager.tutorialStringText = "";
                TutorialManager.tutorialStep = 2;
            }
        }

        public override bool IsGrabbable() {
            return true;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softForce) {
                return;
            }

            PlayHitSound();
            ScoreManager.scoreCount += 40;
            Destroy(gameObject, 6F);
            SetDestroyed(true);

            if (TutorialManager.tutorialStep == 1) {
                TutorialManager.tutorialStringText = "";
                TutorialManager.tutorialStep = 2;
            }
        }

        private void PlayHitSound() {
            AudioSource hitSound = soundAlternator
                ? hitSound1
                : hitSound2;
            AudioManager.PlayAudioSource(hitSound, transform);

            soundAlternator = !soundAlternator;
        }

        private void ReplaceToRagdoll(Vector3 colPosition, float colForce)
        {
            var thisTransform = transform;
            Destroy(gameObject);
            var ragdollGameObject = Instantiate(ragdollPrefab, thisTransform.position, thisTransform.rotation);
            ragdollGameObject.GetComponent<AstronautRagdoll>().KnockBack(colPosition, colForce);
        }
    }
}
