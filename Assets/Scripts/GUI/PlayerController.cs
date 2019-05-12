using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    class PlayerController : MonoBehaviour
    {
        void Awake()
        {
            var fairies = new List<Fairy>();
            var tag = string.Empty;
            var tagSpell = string.Empty;
            
            fairies = GameDataManager.Instance.PlayerData.ActiveFairies;
            tag = "Player Fairy";
            tagSpell = "Player Spell";
            

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var fairy = gameObject.transform.GetChild(i);
                fairy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairies[i].Name}");
                fairy.name = fairies[i].Name;
                fairy.tag = tag;
                var spells = fairies[i].Spells;

                for (int j = 0; j < fairy.transform.childCount; j++)
                {
                    var spell = fairy.transform.GetChild(j);

                    if (DataOfModels.OffensiveSpells.ContainsKey(spells[j]))
                    {
                        spell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/Offensive Spells/{spells[j]}");
                        spell.name = spells[j];
                        spell.tag = tagSpell;
                    }
                    if (DataOfModels.DefensiveSpells.ContainsKey(spells[j]))
                    {
                        spell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/Defensive Spells/{spells[j]}");
                        spell.name = spells[j];
                        spell.tag = tagSpell;
                    }

                    
                }

            }
        }
    }
}
