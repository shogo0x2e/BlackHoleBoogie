using UnityEngine;

namespace Utils {
    public abstract class Vector {
        public static Vector3 GetRandomPosition(float dx, float dy, float dz) {
            float x = Random.Range(-dx, dx);
            float y = Random.Range(-dy, dy);
            float z = Random.Range(-dz, dz);

            return new Vector3(x, y, z);
        }

        public static Vector3 GetRandomDirection() {
            return new Vector3(Random.Range(0F, 1F),
                Random.Range(0F, 1F),
                Random.Range(0F, 1F)).normalized;
        }
    }
}
