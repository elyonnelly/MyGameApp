using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class BattleOffensiveSpellController : MonoBehaviour, IDragHandler
    {
        // Start is called before the first frame update
        public GameObject Sparkle;
        public GameObject Info;
        public int Number;
        public bool Inactive;

        private bool isDrag;
        private bool isEmpty;
        private Vector3 initialPosition;
        private Vector3 offset;
        private List<GameObject> sparkles;
        private int deltaTime;
        private Spell spell;
        private int fairyNumber;

        public void OnMouseDown()
        {
            if (isEmpty || Inactive || spell.Mana == 0)
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

        public void OnMouseDrag()
        {
            if (isEmpty || Inactive || !isDrag || spell.Mana == 0)
            {
                return;
            }

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            if (deltaTime % 10 == 0)
            {
                var sparkle = Instantiate(Sparkle, curPosition, Quaternion.identity);
                sparkle.GetComponent<SparkleComponent>().AttackingFairy = transform.GetComponentInParent<FairyController>().Number;
                sparkle.GetComponent<SparkleComponent>().Spell = name;

                sparkles.Add(sparkle);
            }
        }

        public void OnMouseUp()
        {
            if (isEmpty || Inactive || spell.Mana == 0)
            {
                return;
            }

            foreach (var sparkle in sparkles)
            {
                Destroy(sparkle);
            }

            isDrag = false;
        }

        private void Start()
        {
            sparkles = new List<GameObject>();
            if (name == "Empty Slot")
            {
                isEmpty = true;
            }

            fairyNumber = GetComponentInParent<FairyController>().Number;
            spell = GameProcessManager.PlayerSpells[fairyNumber, Number];

            Info.GetComponent<Text>().text = spell.Mana == 0 ? "-" : spell.Mana.ToString();

            EventAggregator.FairyAttack += EventAggregatorFairyAttack;
        }

        private void OnDisable()
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

        private void Update()
        {
            deltaTime++;
            if (GameDataManager.Instance.PlayerData.ActiveFairies[fairyNumber].IsDead)
            {
                Inactive = true;
            }

            Info.GetComponent<Text>().text = spell.Mana == 0 ? "-" : spell.Mana.ToString();
        }
    }
}