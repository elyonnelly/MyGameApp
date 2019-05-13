using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Sparkle;
        public GameObject Mana;
        public int Number;
        public bool Inactive;

        private bool isDrag;
        private bool isEmpty;
        private Vector3 initialPosition;
        private Vector3 offset;
        private List<GameObject> sparkles;
        private int deltaTime = 0;
        private Spell spell;


        void Start()
        {
            sparkles = new List<GameObject>();
            if (name == "Empty Slot")
            {
                isEmpty = true;
            }


            var fairyNumber = GetComponentInParent<FairyComponent>().Number;
            spell = GameProcessManager.PlayerSpells[fairyNumber, Number];

            Mana.GetComponent<Text>().text = spell.Mana.ToString();

            EventAggregator.FairyAttack += EventAggregatorFairyAttack;
        }

        void OnDisable()
        {
            EventAggregator.FairyAttack -= EventAggregatorFairyAttack;
        }

        private void EventAggregatorFairyAttack(int forwardFairy, int victimFairy, string spell, string victim)
        {

            foreach (var sparkle in sparkles)
            {
                Destroy(sparkle);
            }

            isDrag = false;
        }

        void OnMouseDown()
        {
            if (isEmpty || Inactive)
            {
                return;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        }

        private void OnMouseDrag()
        {

            if (isEmpty || Inactive || !isDrag)
            {
                return;
            }

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            if (deltaTime % 10 == 0)
            {
                var sparkle = Instantiate(Sparkle, curPosition, Quaternion.identity);
                sparkle.GetComponent<Sparkle>().AttackingFairy = transform.GetComponentInParent<FairyComponent>().Number;
                sparkle.GetComponent<Sparkle>().Spell = name;

                sparkles.Add(sparkle);
            }
        }
        private void OnMouseUp()
        {

            if (isEmpty || Inactive)
            {
                return;
            }

            foreach (var sparkle in sparkles)
            {
                Destroy(sparkle);
            }
            isDrag = false;
        }

        void Update()
        {
            deltaTime++;
            Mana.GetComponent<Text>().text = spell.Mana.ToString();
        }
    }
}
