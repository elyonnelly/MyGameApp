using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts
{
    public class DataLoadController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            //TODO добавить десериализацию данных о игроке
            var activeFairies = new List<Fairy>
            {
                (Fairy)DataOfModels.Fairies["Tadana"].Clone(),
                (Fairy)DataOfModels.Fairies["Sillia"].Clone(),
                (Fairy)DataOfModels.Fairies["Vesbat"].Clone(),
                (Fairy)DataOfModels.Fairies["Rasrow"].Clone(),
                (Fairy)DataOfModels.Fairies["Dracwin"].Clone()
            };
            var allowFairies = new List<Fairy>();
            foreach (var fairy in DataOfModels.Fairies)
            {
                allowFairies.Add((Fairy)fairy.Value.Clone());
            }
            var player = new Player(activeFairies, allowFairies, 0);

            //Собрали игрока из данных и загрузили в соответствующий объект
            //GameObject.FindGameObjectWithTag("GameDataManager").GetComponent<GameDataManager>().PlayerData = player;
            GameDataManager.Instance.PlayerData = player; //Так точно работает??
        }

    }
}
