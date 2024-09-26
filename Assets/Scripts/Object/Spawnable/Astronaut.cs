using UnityEngine;
using Utils;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
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

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            if (IsDestroyed() || colForce < softForce) {
                return;
            }

            PlayHitSound();
            ScoreManager.scoreCount += 100;
            Destroy(gameObject, 6F);
            SetDestroyed(true);
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
        }

        private void PlayHitSound() {
            AudioSource hitSound = soundAlternator
                ? hitSound1
                : hitSound2;
            AudioManager.PlayAudioSource(hitSound, transform);

            soundAlternator = !soundAlternator;
        }
    }
}
