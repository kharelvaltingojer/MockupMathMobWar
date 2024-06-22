using System.Collections.Generic;
using Game.Scripts.enums;

namespace Game.Scripts.Utils
{
    public static class Constants
    {
        public static IList<CharacterPowerEnum> NotRandomCharacters = new List<CharacterPowerEnum>
        {
            CharacterPowerEnum.common,
            CharacterPowerEnum.boss,
            CharacterPowerEnum.playerMini,
            CharacterPowerEnum.player,
        };

	public static int MinionsGiftNumber = 40;
    }
}
