using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;

        private Transform target;
        private float timeSinceLastAttack = 0;

        private Mover myMover;
        private ActionScheduler myActionScheduler;
        private Animator myAnimator;

        private void Awake()
        {
            myMover= GetComponent<Mover>();
            myActionScheduler = GetComponent<ActionScheduler>();
            myAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInRange())
            {
                myMover.MoveTo(target.position);
            }
            else
            {
                myMover.Cancel();
                AttackBehaviour();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            myActionScheduler.startAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                myAnimator.SetTrigger(AnimatorParameters.Attack);
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        // Animation Event
        private void Hit()
        {
            
        }
    }
}