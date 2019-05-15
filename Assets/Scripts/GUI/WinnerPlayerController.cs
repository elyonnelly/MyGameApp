using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class WinnerPlayerController : MonoBehaviour
    {
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

        
    }
}
