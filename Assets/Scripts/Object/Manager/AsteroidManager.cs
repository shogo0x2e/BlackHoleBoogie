using UnityEngine;

namespace Object.Manager {
    [CreateAssetMenu(fileName = "AsteroidManager", menuName = "Object/AsteroidManager", order = 1)]
    public class AsteroidManager : ScriptableObject {
        [SerializeField] private GameObject[] asteroidModels;

        public GameObject[] GetAsteroidModels() {
            return asteroidModels;
        }
    }
}
