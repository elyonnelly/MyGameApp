using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleDefensiveSpell : MonoBehaviour
    {
        public Text Info;

         void Start()
        {
            if (DataOfModels.DefensiveSpells.ContainsKey(name))
            {
                Info.GetComponent<Text>().text = DataOfModels.DefensiveSpells[name].Mana.ToString();
            }
        }
    }
}