using UnityEngine;

namespace Utils {
    public abstract class ParticleManager {
        private const float particleMaxDuration = 4F;

        public static void PlayParticle(GameObject particle, Vector3 position) {
            GameObject particleInstance =
                UnityEngine.Object.Instantiate(particle, position, Quaternion.identity);
            UnityEngine.Object.Destroy(particleInstance, particleMaxDuration);
        }
    }
}
