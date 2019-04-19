namespace Assets.Scripts.GameLogic.DataModels
{
    public class DefensiveSpell : Spell
    {
        public DefensiveSpell(Element majorElement, int level, string name, int mana, Element minorElement = default)
            : base(majorElement, level, name, mana, minorElement)
        {

        }
    }
}