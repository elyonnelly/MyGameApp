using Assets.Scripts.GameLogic;

namespace Assets.Scripts
{
    using UnityEngine;

    public class GameDataManager :  Singleton<GameDataManager>
    {
        public GameObject Fairy;
        public Player PlayerData;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {

        }
    }
}
