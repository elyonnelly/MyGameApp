using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;

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
            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
        }

        void Update()
        {

        }

        void OnDisableFairy(string name)
        {
            PlayerData.ActiveFairies.Remove(Fairies.ListOfFairies[name]);
        }

        void OnActivateFairy(string name)
        {
            PlayerData.ActiveFairies.Add(Fairies.ListOfFairies[name]);
        }

        public bool FairyActive(string name)
        {
            foreach (var fairy in PlayerData.ActiveFairies)
            {
                if (name == fairy.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
