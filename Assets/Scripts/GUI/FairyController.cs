using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class FairyController : MonoBehaviour
    {
        public Text FairyInfo;
        public int Number;
        private Fairy fairy;
        void Start()
        {
            fairy = GameDataManager.Instance.PlayerData.ActiveFairies[Number];
            FairyInfo.text = fairy.GetState();
        }

        void Update()
        {
            FairyInfo.text = fairy.GetState();
        }
    }
}
