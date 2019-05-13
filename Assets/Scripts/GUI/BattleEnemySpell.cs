using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleEnemySpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text Info;
        public int Number;
        private Spell spell;

        void Start()
        {
            var fairyNumber = GetComponentInParent<EnemyFairyComponent>().Number;
            spell = GameProcessManager.EnemySpells[fairyNumber, Number];
            if (SceneManager.GetActiveScene().name == "PreBattle")
            {
                //Debug.Log(name);
                Info.GetComponent<Text>().text = "";
                if (DataOfModels.DefensiveSpells.ContainsKey(name))
                {
                    Info.GetComponent<Text>().text = DataOfModels.DefensiveSpells[name].Info;
                }

                if (DataOfModels.OffensiveSpells.ContainsKey(name))
                {
                    Info.GetComponent<Text>().text = DataOfModels.OffensiveSpells[name].Info;
                }
            }
            else
            {

                Info.GetComponent<Text>().text = spell.Mana.ToString();
            }
        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Battlefield Scene")
            {
                Info.GetComponent<Text>().text = spell.Mana.ToString();
            }
        }
    }
}
