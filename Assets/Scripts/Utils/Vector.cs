using UnityEngine;

namespace Utils {
    public abstract class Vector {
        public static Vector3 GetRandomPosition(float dx, float dy, float dz) {
            Vector3 rdPosition = Random.onUnitSphere;
            rdPosition.x *= dx;
            rdPosition.y *= dy;
            rdPosition.z *= dz;
            return rdPosition;
        }

        public static Vector3 GetRandomDirection() {
            return new Vector3(Random.Range(0F, 1F),
                Random.Range(0F, 1F),
                Random.Range(0F, 1F)).normalized;
        }
    }
}
