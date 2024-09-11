using UnityEngine;

namespace Utils {
    public abstract class Vector {
        public static Vector3 GetRandomPosition(float dx, float dy, float dz) {
            Vector3 randomPosition = Random.onUnitSphere;
            randomPosition.x *= dx;
            randomPosition.y *= dy;
            randomPosition.z *= dz;
            return randomPosition;
        }

        public static Vector3 GetRandomDirection() {
            return Random.onUnitSphere;
        }
    }
}
