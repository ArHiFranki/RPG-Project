using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    [RequireComponent(typeof(Mover))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform target;
        private Mover myMover;

        private void Awake()
        {
            myMover= GetComponent<Mover>();
        }

        private void Update()
        {
            if (target == null) return;

            if (!GetIsInRange())
            {
                myMover.MoveTo(target.position);
            }
            else
            {
                myMover.StopMoving();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {
            target = null;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
    }
}