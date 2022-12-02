using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Fighter))]
    [RequireComponent(typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
        private Mover myMover;
        private Fighter myFighter;
        private Health myHealth;

        private void Awake()
        {
            myMover = GetComponent<Mover>();
            myFighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
        }

        private void Update()
        {
            if (myHealth.IsDead) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                hit.transform.TryGetComponent(out CombatTarget target);

                if (target == null) continue;
                if (!myFighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    myFighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    myMover.StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}