using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Mover))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Fighter myFighter;
        private Health myHealth;
        private Mover myMover;
        private GameObject player;

        private Vector3 guardPosition;

        private void Awake()
        {
            myFighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
            myMover = GetComponent<Mover>();
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
                myFighter.Attack(player);
            }
            else
            {
                myMover.StartMoveAction(guardPosition);
            }
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