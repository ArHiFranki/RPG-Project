using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ActionScheduler))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;

        private Fighter myFighter;
        private Health myHealth;
        private Mover myMover;
        private ActionScheduler myActionScheduler;
        private GameObject player;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private int currentWaypointIndex = 0;

        private void Awake()
        {
            myFighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
            myMover = GetComponent<Mover>();
            myActionScheduler = GetComponent<ActionScheduler>();
            player = GameObject.FindWithTag(Tags.Player);
        }

        private void Start()
        {
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (myHealth.IsDead) return;

            if (InAttackRangeOfPlayer() && myFighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            myFighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            myActionScheduler.CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypointPosition();
            }

            myMover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypointPosition());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypointPosition()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}