using UnityEngine;

namespace Utils {
    public abstract class Vector {
        public static Vector3 GetRandomPosition(float dx, float dy, float dz) {
            return Vector3.Scale(Random.onUnitSphere, new Vector3(dx, dy, dz));
        }

        public static Vector3 GetRandomDirection() {
            return Random.onUnitSphere;
        }
    }
}
