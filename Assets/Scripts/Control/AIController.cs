using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(Health))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Fighter myFighter;
        private Health myHealth;
        private GameObject player;

        private void Awake()
        {
            myFighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
            player = GameObject.FindWithTag(Tags.Player);
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
                myFighter.Cancel();
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