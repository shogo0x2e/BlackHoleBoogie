using UnityEngine;

namespace Object.Spawnable {
    public class Astronaut : AbstractObject {
        [SerializeField] private AudioSource hitSound1;
        [SerializeField] private AudioSource hitSound2;
        
        private bool hitsound1played;

        public override void OnHeadCollision(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);
        }

        public override void OnSlap(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }

        public override bool IsGrabbable() {
            return true;
        }

        public override void OnPunch(Vector3 colPosition, float colForce) {
            KnockBack(colPosition, colForce);

            Destroy(gameObject, 6F);
        }

                void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.GetComponent<AbstractObject>() != null)
            {
                return; // Do not explode when colliding with other space objects
            }

            if(!hitsound1played){
                AudioManager.PlayAudioSource(hitSound1,transform);
                
            }
            else {
                AudioManager.PlayAudioSource(hitSound2,transform);
            }
            hitsound1played = !hitsound1played;
        }
    }
}
