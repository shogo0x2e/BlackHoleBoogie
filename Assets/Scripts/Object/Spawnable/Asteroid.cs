using Object.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Object.Spawnable {
    public class Asteroid : AbstractObject {
        [SerializeField] private AsteroidManager asteroidManager;
        
        private GameObject currentModel;
        private bool broken = false;
        
        private const float explForceFactor = 80F;

        public override void OnSpawn() {
            GameObject[] asteroidModels = asteroidManager.GetAsteroidModels();
            GameObject selectedModel = asteroidModels[Random.Range(0, asteroidModels.Length)];
            currentModel = Instantiate(selectedModel, transform.position, transform.rotation);
            currentModel.transform.parent = transform;
        }

        public void OnCollisionEnter(Collision collision) {
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

            broken = true;
            SetMoveSpeed(0F); // TODO: Temporary Fix
            
            Destroy(gameObject, 2F);
        }
    }
}
