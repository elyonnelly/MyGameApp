﻿using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ActiveFairies : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var i = 0;
            Debug.Log(GameDataManager.Instance.PlayerData);

            foreach (var fairy in GameDataManager.Instance.PlayerData.ActiveFairies)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}/{fairy.Name}");
                transform.GetChild(i).name = fairy.Name;
                i++;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}