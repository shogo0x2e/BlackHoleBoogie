using Object.Manager;
using UnityEngine;
using Utils;

namespace Object {
    public class Spawner : MonoBehaviour {
        public SpawnableManager spawnableManager;

        private const float xzRange = 10F;
        private const float y = 0F;

        public void FixedUpdate() {
            if (Random.Range(0F, 1F) < 0.02F) {
                SpawnRandomObject();
            }
        }

        private void SpawnRandomObject() {
            AbstractObject rdObject = spawnableManager.objects[Random.Range(0, spawnableManager.objects.Length)];
            Instantiate(rdObject, Vector.GetRandomPosition(xzRange, y, xzRange), Quaternion.identity);
        }
    }
}
