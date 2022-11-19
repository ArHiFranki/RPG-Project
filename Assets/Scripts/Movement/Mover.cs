using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private NavMeshAgent myNavMeshAgent;
        private Ray lastRay;

        private void Awake()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            Debug.DrawRay(lastRay.origin, lastRay.direction * 100f, Color.yellow);

            myNavMeshAgent.destination = target.position;
        }
    }
}