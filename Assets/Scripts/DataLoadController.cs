using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{
    public class DataLoadController : MonoBehaviour
    {
        /*void Awake()
        {
            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                   //var player = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                   GameDataManager.Instance.PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                }
                using (var reader = new StreamReader(@"enemy.json"))
                {
                    //var player = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    GameDataManager.Instance.EnemyData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }*/

    }
}
