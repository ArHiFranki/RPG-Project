using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicTrigger : MonoBehaviour
    {
        private PlayableDirector myPlayableDirector;
        private bool alreadyTriggered = false;

        private void Awake()
        {
            myPlayableDirector = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.TryGetComponent(out PlayerController playerController))
            {
                alreadyTriggered = true;
                myPlayableDirector.Play();
            }
        }
    }
}