using Object.Manager;
using Object.Spawnable;
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
        private const float alienRange = 4F;

        private const float spawnRate = 0.0092F;

        [SerializeField] private int maxObjectCount = 10;
        private int currentObjectCount = 0;

        public void Start() {
            instance = this;
        }

        public void FixedUpdate() {
            if (BlackHole.paused) return; // TODO: Remove when menu is implemented

            // if (currentObjectCount < maxObjectCount && Random.Range(0F, 1F) < spawnRate) {
            if (Random.Range(0F, 1F) < spawnRate) {
                SpawnRandomObject();
            }
        }

        private void SpawnRandomObject() {
            AbstractObject[] objects = spawnableManager.GetObjects();
            AbstractObject randomObject = objects[Random.Range(0, objects.Length)];

            float xRange = xzRange;
            float zRange = xzRange;

            if (randomObject is Alien) {
                xRange -= alienRange;
                zRange -= alienRange;
            }

            Vector3 spawnPosition = Vector.GetRandomPosition(xRange, y, zRange);

            if (randomObject is Alien) {
                spawnPosition.x += spawnPosition.x >= 0 ? alienRange : -alienRange;
                spawnPosition.z += spawnPosition.z >= 0 ? alienRange : -alienRange;
            }

            Instantiate(randomObject, spawnPosition, Quaternion.identity);
            currentObjectCount++;
        }

        public void OnObjectGettingSucked() {
            currentObjectCount--;
        }
    }
}
