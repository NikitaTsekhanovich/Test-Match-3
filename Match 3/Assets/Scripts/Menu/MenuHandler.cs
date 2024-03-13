using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
