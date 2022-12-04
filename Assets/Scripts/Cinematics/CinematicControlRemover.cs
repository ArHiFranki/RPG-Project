using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;
        private PlayableDirector myPlayableDirector;
        private ActionScheduler myActionScheduler;
        private PlayerController myPlayerController;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            myActionScheduler = player.GetComponent<ActionScheduler>();
            myPlayerController = player.GetComponent<PlayerController>();
            myPlayableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            myPlayableDirector.played += DisableControl;
            myPlayableDirector.stopped += EnableControl;
        }

        private void OnDisable()
        {
            myPlayableDirector.played -= DisableControl;
            myPlayableDirector.stopped -= EnableControl;
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
            myActionScheduler.CancelCurrentAction();
            myPlayerController.enabled = false;
        }

        private void EnableControl(PlayableDirector playableDirector)
        {
            myPlayerController.enabled = true;
        }
    }
}