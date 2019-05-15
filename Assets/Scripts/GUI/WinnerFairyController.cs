using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class WinnerFairyController : MonoBehaviour
    {

        public Text Info;
        public int Number;
        void Start()
        {
            var fairy = GameDataManager.Instance.PlayerData.ActiveFairies[Number];
            Info.text = $"Experience points: {fairy.ExperiencePoints}  Level: {fairy.Level}";
        }

    }
}
