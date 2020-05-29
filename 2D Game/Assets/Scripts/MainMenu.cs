using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

  public class MainMenu : MonoBehaviour
  {
    [SerializeField] private Animator m_FadeAnimator;
      private void Start()
      {
        GameManager.SetGameState(GameManager.GameState.MainMenu);
      }

      public IEnumerator FadeOutToLevel(string level)
      {
        m_FadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneLoader.LoadLevel(level);
      }

      public void LoadLevel(string level)
      {
        StartCoroutine(FadeOutToLevel(level));
      }
    }
