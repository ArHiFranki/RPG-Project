using UnityEngine;
using RPG.Animation;

namespace RPG.Core
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ActionScheduler))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private bool isDead = false;
        private Animator myAnimator;
        private ActionScheduler myActionScheduler;

        public bool IsDead => isDead;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            myAnimator.SetTrigger(AnimatorParameters.Die);
            myActionScheduler.CancelCurrentAction();
        }
    }
}