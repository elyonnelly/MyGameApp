using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts
{
    public class DataLoadController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //TODO добавить десериализацию данных о игроке
            var activeFairies = new List<Fairy>
            {
                (Fairy)Fairies.ListOfFairies["Tadana"].Clone(),
                (Fairy)Fairies.ListOfFairies["Sillia"].Clone(),
                (Fairy)Fairies.ListOfFairies["Vesbat"].Clone(),
                (Fairy)Fairies.ListOfFairies["Rasrow"].Clone(),
                (Fairy)Fairies.ListOfFairies["Dracwin"].Clone()
            };
            var allowFairies = new List<Fairy>();
            foreach (var fairy in Fairies.ListOfFairies)
            {
                allowFairies.Add((Fairy)fairy.Value.Clone());
            }
            var player = new Player(activeFairies, allowFairies, 0);

            //Собрали игрока из данных и загрузили в соответствующий объект
            //GameObject.FindGameObjectWithTag("GameDataManager").GetComponent<GameDataManager>().PlayerData = player;
            GameDataManager.Instance.PlayerData = player; //Так точно работает??
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
