using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleDefensiveSpell : MonoBehaviour
    {
        public Text Info;
        public int Number;

        private Spell spell;
         void Start()
        {
            var fairyNumber = GetComponentInParent<FairyComponent>().Number;
            spell = GameProcessManager.PlayerSpells[fairyNumber, Number];

            Info.GetComponent<Text>().text = spell.Mana.ToString();
        }

         void Update()
         {

             Info.GetComponent<Text>().text = spell.Mana.ToString();
        }
    }
}