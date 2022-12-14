using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            FollowTargetPosition();
        }

        private void FollowTargetPosition()
        {
            transform.position = target.position;
        }
    }
}