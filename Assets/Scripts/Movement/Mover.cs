using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private NavMeshAgent myNavMeshAgent;

        private void Awake()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            myNavMeshAgent.destination = target.position;
        }
    }
}