using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private MonoBehaviour currentAction;

        public void startAction(MonoBehaviour action)
        {
            if (currentAction == action) return;

            if (currentAction != null)
            {
                Debug.Log("Cancelling " + currentAction);
            }
            
            currentAction = action;
        }
    }
}