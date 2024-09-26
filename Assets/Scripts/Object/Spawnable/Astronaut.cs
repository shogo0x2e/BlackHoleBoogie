using UnityEngine;
using Utils;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
        [SerializeField] private AudioSource hitSound1;
        [SerializeField] private AudioSource hitSound2;
        private bool soundAlternator;

        public override void OnHeadCollision(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);
        }

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);
            PlayHitSound();

            if (colForce > softForce) {
                Destroy(gameObject, 6F);
            }
        }

        public override bool IsGrabbable() {
            return true;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);
            PlayHitSound();

            if (colForce > softForce) {
                Destroy(gameObject, 6F);
            }
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
