using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GUI
{
    public class ButtonController : MonoBehaviour
    {
        // Start is called before the first frame update
        public string SceneName;

        public void LoadScene()
        {
            SceneManager.LoadScene(SceneName);
        }

    }
}
