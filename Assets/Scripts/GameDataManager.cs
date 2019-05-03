using System;
using System.IO;
using Assets.Scripts.GameLogic;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{

    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;
        public Player PlayerData;
        public Player EnemyData;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);

            Initialize();

        }


        void Initialize()
        {
            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                    PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                }
                using (var reader = new StreamReader(@"enemy.json"))
                {
                    EnemyData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        
    }
}
