using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleDefensiveSpellController : MonoBehaviour
    {
        public Text Info;
        public int Number;

        private Spell spell;

        private void Start()
        {
            var fairyNumber = GetComponentInParent<FairyController>().Number;
            spell = GameProcessManager.PlayerSpells[fairyNumber, Number];

            Info.GetComponent<Text>().text = spell.Mana.ToString();
        }

        private void Update()
        {
            Info.GetComponent<Text>().text = spell.Mana.ToString();
        }
    }
}