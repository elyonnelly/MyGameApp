using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts
{

    public class GameDataManager :  Singleton<GameDataManager>
    {
        public Player PlayerData;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
        }

        void Update()
        {

        }

        void OnRemoveSpell(int fairyPosition, int spellPosition, string name)
        {
            PlayerData.ActiveFairies[fairyPosition].Spells[spellPosition] = new Spell();
        }

        void OnAddSpell(int fairyPosition, int spellPosition, string name)
        {
            PlayerData.ActiveFairies[fairyPosition].Spells[spellPosition] = (Spell)DataOfModels.Spells[name].Clone();
        }

        void OnDisableFairy(int position, string name)
        {
            PlayerData.ActiveFairies[position] = new Fairy();
        }

        void OnActivateFairy(int position, string name)
        {
            PlayerData.ActiveFairies[position] = (Fairy)(DataOfModels.Fairies[name]).Clone();
        }

        public bool FairyActive(string name)
        {
            foreach (var fairy in PlayerData.ActiveFairies)
            {
                if (name == fairy.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
