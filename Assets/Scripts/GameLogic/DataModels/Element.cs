
using System.Runtime.Serialization;

namespace Assets.Scripts.GameLogic.DataModels
{
    [DataContract]
    public enum Element
    {
        Water,
        Air,
        Stone,
        Ice,
        Metal,
        Fire,
        Nature,
        Psi,
        Light,
        Dark,
        Chaos,
        Energy
    }
}