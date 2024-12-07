using Military.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Military.Scripts.Managers
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}