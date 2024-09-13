using Object.Manager;
using UnityEngine;
using Utils;

namespace Object {
    public class Spawner : MonoBehaviour {
        [SerializeField] private SpawnableManager spawnableManager;

        private const float xzRange = 10F;
        private const float y = 0F;

        public void Start() {
            LifeManager.lifeCount = 3;
        }
        
        public void FixedUpdate() {
            if (Random.Range(0F, 1F) < 0.02F) {
                SpawnRandomObject();
            }
        }

        private void SpawnRandomObject() {
            AbstractObject[] objects = spawnableManager.GetObjects();
            AbstractObject randomObject = objects[Random.Range(0, objects.Length)];
            Instantiate(randomObject, Vector.GetRandomPosition(xzRange, y, xzRange), Quaternion.identity);
        }
    }
}
