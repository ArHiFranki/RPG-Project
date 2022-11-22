using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(ActionScheduler))]
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent myNavMeshAgent;
        private Animator myAnimator;
        private Fighter myFighter;
        private ActionScheduler myActionScheduler;

        private void Awake()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myFighter = GetComponent<Fighter>();
            myActionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            myActionScheduler.startAction(this);
            myFighter.CancelAttack();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            myNavMeshAgent.destination = destination;
            myNavMeshAgent.isStopped = false;
        }

        public void StopMoving()
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