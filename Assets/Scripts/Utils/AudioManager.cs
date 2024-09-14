using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Utils {
    public abstract class AudioManager {
        public static void PlayAudioSource(AudioSource audioSource, Transform transform) {
            AudioSource audioSourceInstance =
                UnityEngine.Object.Instantiate(audioSource, transform);
            audioSourceInstance.Play();

            UnityEngine.Object.Destroy(audioSourceInstance.gameObject, audioSourceInstance.clip.length);
        }
    }
}
