using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class SparkleComponent : MonoBehaviour
    {
        public string Spell;
        public int AttackingFairy;
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (tag == "Player Spell" && collider.tag == "Enemy Fairy")
            {
                EventAggregator.OnFairyAttack(AttackingFairy,
                                                   collider.GetComponent<EnemyFairyController>().Number, Spell,
                                                   collider.transform.parent.tag);
            }
        }

    }
}
