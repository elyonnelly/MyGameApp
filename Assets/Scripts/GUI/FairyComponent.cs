using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class FairyComponent : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text FairyInfo;
        public int Number;
        private Fairy fairy;
        void Start()
        {
            if (tag == "Enemy Fairy")
            {
                fairy = GameDataManager.Instance.EnemyData.ActiveFairies[Number];
            }
            else
            {
                fairy = GameDataManager.Instance.PlayerData.ActiveFairies[Number];
            }
            FairyInfo.text = fairy.GetState();
        }

        // Update is called once per frame
        void Update()
        {
            FairyInfo.text = fairy.GetState();
        }
    }
}
