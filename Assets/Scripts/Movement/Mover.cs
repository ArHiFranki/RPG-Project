using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Animation;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(Health))]
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent myNavMeshAgent;
        private Animator myAnimator;
        private ActionScheduler myActionScheduler;
        private Health myHealth;

        private void Awake()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
            myHealth = GetComponent<Health>();
        }

        private void Update()
        {
            myNavMeshAgent.enabled = !myHealth.IsDead;

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            myActionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            myNavMeshAgent.destination = destination;
            myNavMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            myNavMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = myNavMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            myAnimator.SetFloat(AnimatorParameters.ForwardSpeed, speed);
        }
    }
}