using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class BattleSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject Sparkle;

        private bool isDrag;
        private Vector3 initialPosition;
        private Vector3 offset;
        private List<GameObject> sparkles;
        private int deltaTime = 0;

        void Start()
        {
            sparkles = new List<GameObject>();
        }
        void OnMouseDown()
        {
            //Debug.Log(name);
            if (DataOfModels.Spells[name] is DefensiveSpell)
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
            if (DataOfModels.Spells[name] is DefensiveSpell)
            {
                return;
            }
            if (!isDrag)
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
            if (DataOfModels.Spells[name] is DefensiveSpell)
            {
                return;
            }

            foreach (var sparkle in sparkles)
            {
                Destroy(sparkle);
            }
            isDrag = false;
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (tag == "Player Spell" && collider.tag == "Enemy Fairy")
            {
                EventAggregator.PublishFairyAttack(transform.GetComponentInParent<FairyComponent>().Number,
                                                   collider.GetComponent<FairyComponent>().Number, name,
                                                   collider.transform.parent.tag);
                transform.position = initialPosition;
                isDrag = false;
            }
        }

        void Update()
        {
            deltaTime++;
        }
    }
}
