using Object.Manager;
using UnityEngine;
using Utils;

namespace Object {
    public class Spawner : MonoBehaviour {
        [SerializeField] private SpawnableManager spawnableManager;

        private const float xzRange = 10F;
        private const float y = 0F;
        [SerializeField] private int maxObjectCount = 10;
        private int currentObjectCount = 0;

        public void Start() {
            LifeManager.lifeCount = 3;
        }
        
        public void FixedUpdate() {
            if (currentObjectCount < maxObjectCount && Random.Range(0F, 1F) < 0.01F) {
                SpawnRandomObject();
                
            }
        }

        private void SpawnRandomObject() {
            AbstractObject[] objects = spawnableManager.GetObjects();
            AbstractObject randomObject = objects[Random.Range(0, objects.Length)];
            AbstractObject instance = Instantiate(randomObject, Vector.GetRandomPosition(xzRange, y, xzRange), Quaternion.identity);

            instance.SetSpawner(this); // Set the spawner reference in the spawned object
            currentObjectCount++;
        }

        public void OnObjectDestroyed(){
            currentObjectCount--;
        }
    }
}
