using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class AllowFairiesController : MonoBehaviour
    {
        public GameObject Fairy;
        public Dictionary<string, GameObject> TableOfFairies;

        private void Awake()
        {
            EventAggregator.DisableFairy += UpdateTable;

            TableOfFairies = new Dictionary<string, GameObject>();
            FillTableOfFairies();

            var player = GameDataManager.Instance.PlayerData;

            foreach (var fairy in player.AllowFairies)
            {
                if (TableOfFairies.ContainsKey(fairy.Name))
                {
                    TableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}");
                    TableOfFairies[fairy.Name].GetComponent<AllowFairyController>().IsAllow = true;
                }
            }

            foreach (var fairy in player.ActiveFairies)
            {
                TableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/EmptyPrefab");
                TableOfFairies[fairy.Name].GetComponent<AllowFairyController>().IsUsed = true;
            }


        }

        private void FillTableOfFairies()
        {
            var fairies = GameDataManager.Instance.PlayerData.AllowFairies;
            var count = 0;
            var flag = false;

            while (count < fairies.Count)
            {
                for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
                {
                    for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                    {
                        if (count >= fairies.Count)
                        {
                            flag = true;
                            break;
                        }

                        TableOfFairies.Add(fairies[count].Name, CreateNewFairyObject(fairies[count].Name, new Vector3(x, y, -1)));
                        count++;
                    }

                    if (flag)
                    {
                        break;
                    }
                }

            }

        }

        private GameObject CreateNewFairyObject(string name, Vector3 position)
        {
            var newFairy = Instantiate(Fairy, position, Quaternion.identity);
            newFairy.transform.SetParent(transform);
            newFairy.SetActive(false);
            newFairy.name = name;
            return newFairy;
        }

        private void UpdateTable(int position, string fairyName)
        {
            TableOfFairies[fairyName].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairyName}");
        }

        private void OnDisable()
        {
            EventAggregator.DisableFairy -= UpdateTable;

        }
    }

}