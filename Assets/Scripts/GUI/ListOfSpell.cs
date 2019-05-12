using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ListOfSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Spell;
        public Dictionary<string, GameObject> TableOfSpells;


        void Start()
        {
            TableOfSpells = new Dictionary<string, GameObject>();
            FillTableOfSpells();
        }

        void FillTableOfSpells()
        {
            var z = transform.position.z;

            var offensiveSpells = GameDataManager.Instance.PlayerData.AllowOffensiveSpells;
            var defensiveSpells = GameDataManager.Instance.PlayerData.AllowDefensiveSpells;

            var count = 0;
            var x1 = 0f;
            var y1 = 0f;
            var flag = false;
            while (count < offensiveSpells.Count)
            {
                for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
                {
                    for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                    {
                        if (count >= offensiveSpells.Count)
                        {
                            x1 = x;
                            y1 = y;
                            flag = true;
                            break;
                        }
                        TableOfSpells.Add(offensiveSpells[count], CreateNewSpellSprite(offensiveSpells[count], new Vector3(x, y, -1), "off"));
                       
                        count++;
                    }

                    if (flag)
                    {
                        break;
                    }
                }
            }

            count = 0;
            for (var x = x1; x < transform.position.x + 6; x += 1)
            {

                if (count < defensiveSpells.Count - 1)
                {
                    TableOfSpells.Add(defensiveSpells[count], CreateNewSpellSprite(defensiveSpells[count], new Vector3(x, y1, -1), "def"));
                }
                count++;
            }

            //count = 0;
            for (var y = y1 -1; y > transform.position.y - 6; y -= 1)
            { 
                for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                {
                    
                    if (count < defensiveSpells.Count - 1)
                    {
                        TableOfSpells.Add(defensiveSpells[count], CreateNewSpellSprite(defensiveSpells[count], new Vector3(x, y, -1), "def"));
                    }
                    count++;
                }
            }

            while (count < defensiveSpells.Count)
            {
                for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
                {
                    for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                    {
                        if (count < defensiveSpells.Count - 1)
                        {
                            TableOfSpells.Add(defensiveSpells[count], CreateNewSpellSprite(defensiveSpells[count], new Vector3(x, y, -1), "def"));
                        }
                        count++;
                    }
                }
            }


            count = 0;
            foreach (var spell in TableOfSpells.Values)
            {
                spell.SetActive(true);
                count++;
                if (count >= 36)
                {
                    break;
                }
            }
        }

        GameObject CreateNewSpellSprite(string name, Vector3 position, string type)
        {
            var newSpell = Instantiate(Spell, position, Quaternion.identity);
            newSpell.SetActive(false);
            newSpell.name = name;
            newSpell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(type == "off" ? $"Sprites/Spells Icon/Offensive Spells/{name}" :
                                                                                        $"Sprites/Spells Icon/Defensive Spells/{name}");
            newSpell.transform.SetParent(transform);
            return newSpell;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
