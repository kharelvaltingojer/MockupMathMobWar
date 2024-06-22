using UnityEngine;

namespace Game.Scripts.Characters
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int spawns = 0;
    }
}
