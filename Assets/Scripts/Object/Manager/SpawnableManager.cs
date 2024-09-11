using UnityEngine;

namespace Object.Manager {
    [CreateAssetMenu(fileName = "SpawnableManager", menuName = "Object/SpawnableManager", order = 1)]
    public class SpawnableManager : ScriptableObject {
        public AbstractObject[] objects;
    }
}
