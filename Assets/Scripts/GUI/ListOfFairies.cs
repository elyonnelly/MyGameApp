using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ListOfFairies : MonoBehaviour
    {
        public GameObject Fairy;
        private Dictionary<string, GameObject> tableOfFairies;

        private void Start()
        {
            EventAggregator.DisableFairy += UpdateTable;

            tableOfFairies = new Dictionary<string, GameObject>();
            FillTableOfFairies();

            var player = GameDataManager.Instance.PlayerData;

            foreach (var fairy in player.AllowFairies)
            {
                tableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}/{fairy.Name}");
                tableOfFairies[fairy.Name].GetComponent<AllowFairy>().IsAllow = true;
            }

            foreach (var fairy in player.ActiveFairies)
            {
                tableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/EmptyPrefab");
                tableOfFairies[fairy.Name].GetComponent<AllowFairy>().IsUsed = true;
            }
        }

        private void FillTableOfFairies()
        {
            var namesOfFairies = new string[DataOfModels.Fairies.Count];

            DataOfModels.Fairies.Keys.CopyTo(namesOfFairies, 0);
            var count = 0;

            for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
            {
                for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                {
                    var name = count < namesOfFairies.Length ? namesOfFairies[count] : $"EmptySlot + {count}";
                    tableOfFairies.Add(name, CreateNewFairySprite(name, new Vector3(x, y, -1)));

                    count++;
                }
            }
        }

        private GameObject CreateNewFairySprite(string name, Vector3 position)
        {
            var newFairy = Instantiate(Fairy, position, Quaternion.identity);
            newFairy.transform.SetParent(transform);
            newFairy.SetActive(false);
            newFairy.name = name;
            return newFairy;
        }


        private void UpdateTable(int position, string fairyName)
        {
            tableOfFairies[fairyName].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairyName}/{fairyName}");
        }
    }

}