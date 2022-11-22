using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform target;
        private Mover myMover;
        private ActionScheduler myActionScheduler;

        private void Awake()
        {
            myMover= GetComponent<Mover>();
            myActionScheduler = GetComponent<ActionScheduler>();
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
            myActionScheduler.startAction(this);
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