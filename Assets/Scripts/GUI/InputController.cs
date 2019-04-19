using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GUI
{
    public class InputController : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject AllowFairies;
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        //TODO сюда мы пишем про обработку, ну например, кнопок на сцене

        public void DisplayAllowFairies()
        {
            for (int i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        public void DisplayAllowSpells()
        {
            for (int i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
