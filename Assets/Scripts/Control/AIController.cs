using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private void Update()
        {
            
            if (DistanceToPlayer() < chaseDistance)
            {
                Debug.Log(gameObject.name + " should chase");
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag(Tags.Player);
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}