using Object.Manager;
using UnityEngine;
using Utils;

namespace Object {
    public class Spawner : MonoBehaviour {
        private static Spawner instance;

        public static Spawner GetInstance() {
            return instance;
        }

        [SerializeField] private SpawnableManager spawnableManager;

        private const float xzRange = 10F;
        private const float y = 1F;

        private const float spawnRate = 0.0092F;

        [SerializeField] private int maxObjectCount = 10;
        private int currentObjectCount = 0;

        public void Start() {
            instance = this;
        }

        public void FixedUpdate() {
            if (BlackHole.paused) return; // TODO: TEMPORARY FIX
            
            // if (currentObjectCount < maxObjectCount && Random.Range(0F, 1F) < spawnRate) {
            if (Random.Range(0F, 1F) < spawnRate) {
                SpawnRandomObject();
            }
        }

        private void SpawnRandomObject() {
            AbstractObject[] objects = spawnableManager.GetObjects();
            AbstractObject randomObject = objects[Random.Range(0, objects.Length)];
            Instantiate(
                randomObject,
                Vector.GetRandomPosition(xzRange, y, xzRange),
                Quaternion.identity);
            currentObjectCount++;
        }

        public void OnObjectGettingSucked() {
            currentObjectCount--;
        }
    }
}
