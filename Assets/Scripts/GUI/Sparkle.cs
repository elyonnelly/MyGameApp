using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class Sparkle : MonoBehaviour
    {
        public string Spell;
        public int AttackingFairy;
        void Start()
        {
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (tag == "Player Spell" && collider.tag == "Enemy Fairy")
            {
                //Debug.Log("hit");
                EventAggregator.OnFairyAttack(AttackingFairy,
                                                   collider.GetComponent<EnemyFairyComponent>().Number, Spell,
                                                   collider.transform.parent.tag);
            }
        }
    }
}
