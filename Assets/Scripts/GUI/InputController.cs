using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GUI
{
    public class InputController : MonoBehaviour
    {
        public GameObject AllowFairies;
        public GameObject AllowSpells;
        public void LoadScene(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "Choise Fairy Scene")
            {
                try
                {
                    using (var writer = new StreamWriter(@"player.json"))
                    {
                        writer.Write(JsonConvert.SerializeObject(GameDataManager.Instance.PlayerData));
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SceneManager.LoadScene(sceneName);
        }
        //TODO сюда мы пишем про обработку, ну например, кнопок на сцене

        public void DisplayAllowFairies()
        {

            for (int i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < AllowSpells.transform.childCount; i++)
            {
                AllowSpells.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void DisplayAllowSpells()
        {
            for (int i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < AllowSpells.transform.childCount; i++)
            {
                AllowSpells.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
