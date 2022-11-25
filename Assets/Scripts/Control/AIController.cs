using UnityEngine;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Fighter))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Mover myMover;
        private Fighter myFighter;
        private GameObject player;

        private void Awake()
        {
            myMover = GetComponent<Mover>();
            myFighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag(Tags.Player);
        }

        private void Update()
        {
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
    }
}