using System;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ListOfSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Spell;
        private Dictionary<string, GameObject> listOfFairies;
        void Start()
        {
            var rectTransform = (RectTransform) transform;
            var player = GameDataManager.Instance.PlayerData;

            rectTransform.sizeDelta = new Vector2(1*rectTransform.rect.width, Fairies.ListOfSpell.Count * (rectTransform.rect.height));

            listOfFairies = new Dictionary<string, GameObject>();

            var namesOfSpells = new string[Fairies.ListOfSpell.Count];
            Fairies.ListOfSpell.Keys.CopyTo(namesOfSpells, 0);

            var x = rectTransform.parent.position.x + 0.6f;
            var y = rectTransform.parent.position.y + 0.5f;
            
            for (var i = 0; i < namesOfSpells.Length; i++)
            {
                var newSpell = Instantiate(Spell, new Vector3(x, y, 0), Quaternion.identity);
                newSpell.transform.SetParent(transform);

                newSpell.name = namesOfSpells[i];

                listOfFairies.Add(newSpell.name, newSpell);

                y -= 1.3f;


            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
