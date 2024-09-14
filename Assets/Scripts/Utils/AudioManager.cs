using System.Collections;
using UnityEngine;

namespace Utils {
    public abstract class AudioManager {
        public static void PlayAudioSource(AudioSource audioSource, Transform transform) {
            AudioSource audioSourceInstance = UnityEngine.Object.Instantiate(
                audioSource,
                transform.position,
                transform.rotation);
            audioSourceInstance.Play();

            // Use BlackHole instance since it will always exist
            BlackHole.GetInstance().StartCoroutine(DestroyAfterPlaying(audioSourceInstance));
        }

        private static IEnumerator DestroyAfterPlaying(AudioSource audioSource) {
            yield return new WaitForSeconds(audioSource.clip.length);

            UnityEngine.Object.Destroy(audioSource.gameObject);
        }
    }
}
