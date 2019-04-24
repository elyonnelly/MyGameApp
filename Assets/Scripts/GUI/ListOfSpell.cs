using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ListOfSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Spell;
        private Dictionary<string, GameObject> tableOfSpells;
        
        void Start()
        {
            tableOfSpells = new Dictionary<string, GameObject>();
            FillTableOfSpells();
        }

        void FillTableOfSpells()
        {
            var x = transform.position.x;
            var y = transform.position.y;
            var z = transform.position.z;

            //лучше куда-то перенести эту информацию определенно
            foreach (var spell in DataOfModels.Spells.Keys)
            {
                tableOfSpells.Add(spell, CreateNewSpellSprite(spell, new Vector3(x, y, z)));

                y += x > 4 ? -1 : 0;
                x = x > 4 ? transform.position.x : x + 1;
            }
        }

        GameObject CreateNewSpellSprite(string name, Vector3 position)
        {
            var newSpell = Instantiate(Spell, position, Quaternion.identity);
            newSpell.name = name;
            newSpell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/{name}");
            newSpell.transform.SetParent(transform);
            return newSpell;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
