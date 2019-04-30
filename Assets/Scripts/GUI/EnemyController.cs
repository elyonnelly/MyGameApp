using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class EnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var fairies = GameDataManager.Instance.EnemyData.ActiveFairies;

            //Debug.Log(gameObject.transform.childCount);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var fairy = gameObject.transform.GetChild(i);
                fairy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairies[i].Name}/{fairies[i].Name}");
                fairy.name = fairies[i].Name;
                //fairy.GetComponent<FairyComponent>().Fairy = GameDataManager.Instance.PlayerData.ActiveFairies[i];
                var spells = fairies[i].Spells;

                for (var j = 0; j < fairy.transform.childCount; j++)
                {
                    var spell = fairy.transform.GetChild(j);

                    Debug.Log(spell.name);
                    if (DataOfModels.Spells.ContainsKey(spells[j].Name))
                    {
                        spell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/{spells[j].Name}");
                        spell.name = spells[j].Name;
                    }
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
