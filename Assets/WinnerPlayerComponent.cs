using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class WinnerPlayerComponent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var player = GameDataManager.Instance.PlayerData;
            var i = 0;
            foreach (var fairy in transform.GetComponentsInChildren< SpriteRenderer>())
            {
                fairy.sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{ player.ActiveFairies[i].Name}");
                fairy.gameObject.name = player.ActiveFairies[i].Name;
                i++;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
