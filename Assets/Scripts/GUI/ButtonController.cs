using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GUI
{
    public class ButtonController : MonoBehaviour
    {
        public string SceneName;

        public void LoadScene()
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
            SceneManager.LoadScene(SceneName);
        }

    }
}
