using System.Runtime.Serialization;

namespace Assets.Scripts.GameLogic.DataModels
{
    [DataContract]
    public class FairyData
    {
        [DataMember]
        public string Name { private set; get; }

        [DataMember]
        public string Description { private set; get; }

        [DataMember]
        public Element Element { private set; get; }

        [DataMember]
        public int LevelForEvolution { private set; get; }

        [DataMember]
        public string EvolvesTo { private set; get; }

    }
}