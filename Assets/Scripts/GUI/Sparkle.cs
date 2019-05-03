using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class Sparkle : MonoBehaviour
    {
        public string Spell;
        public int AttackingFairy;
        void Start()
        {
            Debug.Log(Spell);

        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (tag == "Player Spell" && collider.tag == "Enemy Fairy")
            {
                EventAggregator.PublishFairyAttack(AttackingFairy,
                                                   collider.GetComponent<FairyComponent>().Number, Spell,
                                                   collider.transform.parent.tag);
            }
        }
    }
}
