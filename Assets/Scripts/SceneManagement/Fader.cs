using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        private CanvasGroup myCanvasGroup;

        private void Awake()
        {
            myCanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            myCanvasGroup.alpha = 0f;
        }

        public IEnumerator FadeOut(float time)
        {
            while(myCanvasGroup.alpha < 1f)
            {
                myCanvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (myCanvasGroup.alpha > 0f)
            {
                myCanvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}