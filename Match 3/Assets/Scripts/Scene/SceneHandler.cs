using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneHandler : MonoBehaviour
    {
        public void LoadNextScene()
        {
            var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            AnimatorLoadingScreen.instance.AnimationFade($"Level{nextLevelIndex}");
        }
        
        public void LoadPreviousScene()
        {
            AnimatorLoadingScreen.instance.AnimationFade("Menu");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
