using Object.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;


namespace Object.Spawnable {
    public class Astronaut : AbstractObject { 

        [SerializeField] private AudioSource hitSound1;
        [SerializeField] private AudioSource hitSound2;
        
        private bool hitsound1played;
    
        private AudioSource audioSource; 
        void Start(){

            audioSource = GetComponent<AudioSource>();
        }

        void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.GetComponent<AbstractObject>() != null)
            {
                return; // Do not explode when colliding with other space objects
            }

            if(!hitsound1played){
                AudioManager.PlayAudioSource(hitSound1,transform);
                
            }
            else {
                AudioManager.PlayAudioSource(hitSound2,transform);
            }
            hitsound1played = !hitsound1played;
        }
     }
}
