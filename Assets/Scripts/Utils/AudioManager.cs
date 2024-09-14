using UnityEngine;

namespace Utils {
    public abstract class AudioManager {
        public static void PlayAudioSource(AudioSource audioSource, Transform transform) {
            AudioSource audioSourceInstance =
                UnityEngine.Object.Instantiate(audioSource, transform.position, transform.rotation);
            audioSourceInstance.Play();

            UnityEngine.Object.Destroy(audioSourceInstance.gameObject, audioSourceInstance.clip.length);
        }
    }
}
