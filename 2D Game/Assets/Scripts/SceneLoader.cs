using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneLoader : MonoBehaviour

    {
        [SerializeField] private Animator m_FadeAmimator;
        private static string nextLevel;

        public static void LoadLevel(string level)
        {
            SceneManager.LoadScene("LoadingScene");
            nextLevel = level;
        }

        private IEnumerator Start()
        {
            GameManager.SetGameState(GameManager.GameState.Loading);
            yield return new WaitForSeconds(3f);
            if (string.IsNullOrEmpty(nextLevel))
            {
                nextLevel = "MainMenu";
            }
            AsyncOperation loading = null;
            loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
            loading.allowSceneActivation = false;
            while (loading.progress<0.9f)
            {
                yield return null;
            }

            m_FadeAmimator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(2f);
            loading.allowSceneActivation = true;
            while (!loading.isDone)
            {
                yield return null;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevel));
            nextLevel = null;
            SceneManager.UnloadSceneAsync("LoadingScene");
            
        }



    }

}