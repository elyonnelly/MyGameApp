using System;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class AllowSpellController : MonoBehaviour, IDragHandler, ICollisionHandler
    {
        public Text Info;
        private Vector3 initialPosition;
        private Vector3 offset;
        private bool isDrag;
        private bool isUsed;


        void Start()
        {
            EventAggregator.RemoveSpell += MakeUnused;
            Info = GameObject.FindGameObjectWithTag("SpellInfo").GetComponent<Text>();
        }

        void OnDisable()
        {
            EventAggregator.RemoveSpell -= MakeUnused;
        }

        public void OnMouseDown()
        {
            if (isUsed)
            {
                return;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            ViewInfo();

        }

        private void ViewInfo()
        {
            if (DataOfModels.OffensiveSpells.ContainsKey(name))
            {
                var spell = DataOfModels.OffensiveSpells[name];
                Info.text = $"{spell.Name}\n\nOffensive Spell of \n{spell.MajorElement.ToString()}\n{spell.Info}\nLevel: {spell.Level} Damage:{spell.Damage} Mana:{spell.Mana}";
            }

            if (DataOfModels.DefensiveSpells.ContainsKey(name))
            {
                var spell = DataOfModels.DefensiveSpells[name];
                Info.text = $"{spell.Name}\n\nDefensive Spell of\n{spell.MajorElement.ToString()}\n{spell.Info}\nLevel: {spell.Level} Mana:{spell.Mana}";
            }
        }

        public void OnMouseUp()
        {
            if (isUsed)
            {
                return;
            }

            initialPosition.z += 2;
            transform.position = initialPosition;
            isDrag = false;
        }

        public void OnMouseDrag()
        {
            if (isUsed)
            {
                return;
            }
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag != "Active Spell")
            {
                return;
            }

            var fairy = collider.GetComponentInParent<ActiveFairyController>();
            var spell = collider.GetComponent<ActiveSpellController>();

            if (spell.Type == "Offensive" && DataOfModels.DefensiveSpells.ContainsKey(name) ||
                spell.Type == "Defensive" && DataOfModels.OffensiveSpells.ContainsKey(name))
            {
                return;
            }

            if (fairy.IsEmpty)
            {
                return;
            }


            if (DataOfModels.OffensiveSpells.ContainsKey(name))
            {
                var offensiveSpell = DataOfModels.OffensiveSpells[name];
                if (offensiveSpell.MajorElement != DataOfModels.Fairies[fairy.name].Element)
                {
                    return;
                }


                if (Math.Ceiling(DataOfModels.Fairies[fairy.name].Level / 6.0) < offensiveSpell.Level)
                {
                    return;
                }
            }

            if (DataOfModels.DefensiveSpells.ContainsKey(name))
            {
                var defensiveSpell = DataOfModels.DefensiveSpells[name];
                if (defensiveSpell.MajorElement != DataOfModels.Fairies[fairy.name].Element &&
                    defensiveSpell.MinorElement != DataOfModels.Fairies[fairy.name].Element)
                {
                    return;
                }
                if (Math.Ceiling(DataOfModels.Fairies[fairy.name].Level / 6.0) < defensiveSpell.Level)
                {
                    return;
                }

            }

            if (!collider.GetComponent<ActiveSpellController>().IsEmpty)
            {
                EventAggregator.OnRemoveSpell(fairy.Number, spell.Number, spell.gameObject.name);
            }
            EventAggregator.OnAddSpell(fairy.Number, spell.Number, gameObject.name);

            ChangeObject(collider.gameObject, gameObject);
            MakeUsed();
        }

        public void ChangeObject(GameObject oldSpell, GameObject newSpell)
        {
            oldSpell.GetComponent<SpriteRenderer>().sprite = newSpell.GetComponent<SpriteRenderer>().sprite;
            oldSpell.name = newSpell.name;
            oldSpell.GetComponent<ActiveSpellController>().IsEmpty = false;
        }

        public void MakeUsed()
        {
            initialPosition.z += 2;
            transform.position = initialPosition;
            isUsed = true;
            isDrag = false;
        }


        void MakeUnused(int fairyPosition, int spellPosition, string name)
        {
            if (gameObject.name == name)
            {
                isUsed = false;
            }
        }
    }
}
