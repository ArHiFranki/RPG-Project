using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {   
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Debug.Log("Scene Loaded");
            Destroy(gameObject);
        }
    }
}