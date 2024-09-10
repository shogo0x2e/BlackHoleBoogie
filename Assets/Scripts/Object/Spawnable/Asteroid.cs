using Object.Manager;
using UnityEngine;

namespace Object.Spawnable {
    public class Asteroid : AbstractObject {
        public AsteroidManager asteroidManager;

        private GameObject currentModel;
        private bool broken = false;

        protected override void OnSpawn() {
            GameObject selectedModel =
                asteroidManager.asteroidModels[Random.Range(0, asteroidManager.asteroidModels.Length)];
            currentModel = Instantiate(selectedModel, transform.position, transform.rotation);
            currentModel.transform.parent = transform;
            
            Explode();
        }

        public void Explode() {
            if (broken) {
                return;
            }

            GameObject repl = Instantiate(currentModel, transform.position, transform.rotation);

            Transform[] trsfs = repl.GetComponentsInChildren<Transform>();
            foreach (Transform trsf in trsfs) {
                if (trsf.GetComponent<Rigidbody>() == null) {
                    trsf.gameObject.AddComponent<Rigidbody>();
                }
            }

            Rigidbody[] rbs = repl.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs) {
                rb.AddExplosionForce(1000, transform.position, 2);
            }

            broken = true;

            Destroy(this);
        }
    }
}
