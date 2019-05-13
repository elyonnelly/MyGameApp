using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class WinnerFairyComponent : MonoBehaviour
    {
        // Start is called before the first frame update

        public Text Info;
        public int Number;
        void Start()
        {
            var fairy = GameDataManager.Instance.PlayerData.ActiveFairies[Number];
            Info.text = $"Experience points: {fairy.ExperiencePoints}  Level: {fairy.Level}";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
