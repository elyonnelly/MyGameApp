using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleEnemySpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text Info;

        void Start()
        {
            Debug.Log(name);
            if (DataOfModels.OffensiveSpells.ContainsKey(name))
            {
                Info.GetComponent<Text>().text = DataOfModels.OffensiveSpells[name].Mana.ToString();
            }
            if (DataOfModels.DefensiveSpells.ContainsKey(name))
            {
                Info.GetComponent<Text>().text = DataOfModels.DefensiveSpells[name].Mana.ToString();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
