using UnityEngine;
using RPG.Animation;

namespace RPG.Combat
{
    [RequireComponent(typeof(Animator))]
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private bool isDead = false;
        private Animator myAnimator;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
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
            isDead = true;
            myAnimator.SetTrigger(AnimatorParameters.Die);
        }
    }
}