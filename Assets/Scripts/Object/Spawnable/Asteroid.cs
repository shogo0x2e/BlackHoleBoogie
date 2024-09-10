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
        }

        public void Explode() {
            if (broken) {
                return;
            }

            GameObject repl = Instantiate(currentModel, transform.position, transform.rotation);

            Transform[] allChildren = repl.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren) {
                if (child.GetComponent<Rigidbody>() == null) {
                    child.gameObject.AddComponent<Rigidbody>();
                }
            }

            Rigidbody[] rbs = repl.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs) {
                rb.AddExplosionForce(100, transform.position, 2);
            }

            broken = true;

            Destroy(gameObject);
        }
    }
}
