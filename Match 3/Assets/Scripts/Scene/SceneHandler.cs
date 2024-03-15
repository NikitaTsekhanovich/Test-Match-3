using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneHandler : MonoBehaviour
    {
        public void LoadNextScene()
        {
            AnimatorLoadingScreen.instance.AnimationFade("Game");
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
