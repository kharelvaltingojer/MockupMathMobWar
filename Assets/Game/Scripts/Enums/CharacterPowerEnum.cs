using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.enums
{
    public enum CharacterPowerEnum
    {
        common = 1,
        normal = 3, // summons: +3 common = 6 total power
        rare = 4,
        epic = 5, // summons: +2 normal = 18 total power
        miniBoss = 10, // summons: +2 epic = 46 total power
        boss = 99, // variable power
        
        playerMini = 999,
        player = 1000,
    }
}