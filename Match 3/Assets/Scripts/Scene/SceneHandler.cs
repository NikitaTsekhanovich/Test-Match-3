using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneHandler : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void LoadPreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
