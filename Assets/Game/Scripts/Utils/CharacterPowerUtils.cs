using System.Collections.Generic;
using Game.Scripts.enums;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.utils
{
    public static class CharacterPowerUtils
    {
        public static IList<CharacterPowerEnum> GetSlavesByPowerList(CharacterPowerEnum power)
        {
            IList<CharacterPowerEnum> slaves = new List<CharacterPowerEnum>();
            switch (power)
            {
                case CharacterPowerEnum.common:
                    break;
                case CharacterPowerEnum.normal:
                    slaves.Add(CharacterPowerEnum.common);
                    slaves.Add(CharacterPowerEnum.common);
                    slaves.Add(CharacterPowerEnum.common);
                    break;
                case CharacterPowerEnum.rare:
                    break;
                case CharacterPowerEnum.epic:
                    slaves.Add(CharacterPowerEnum.normal);
                    slaves.Add(CharacterPowerEnum.normal);
                    break;
                case CharacterPowerEnum.miniBoss:
                    slaves.Add(CharacterPowerEnum.epic);
                    slaves.Add(CharacterPowerEnum.epic);
                    break;
                case CharacterPowerEnum.boss:
                    break;
                case CharacterPowerEnum.playerMini:
                    break;
                case CharacterPowerEnum.player:
                    break;
                default:
                    break;
            }

            return slaves;
        }
        
        public static int GetTotalPower(CharacterPowerEnum power)
        {
            int totalPower = (int)power;
            
            IList<CharacterPowerEnum> slaves = GetSlavesByPowerList(power);
            
            if (slaves.Count > 0)
            {
                foreach (CharacterPowerEnum slave in slaves)
                {
                    totalPower += GetTotalPower(slave);
                }
            }

            return totalPower;
        }
        
        public static CharacterPowerEnum GetThePowerfulCharacter(int availablePower)
        {
            CharacterPowerEnum power = CharacterPowerEnum.common;
            int powerValue = 0;
            foreach (CharacterPowerEnum powerEnum in (CharacterPowerEnum[])System.Enum.GetValues(typeof(CharacterPowerEnum)))
            {
                int totalPower = GetTotalPower(powerEnum);
                if (totalPower <= availablePower && totalPower > powerValue)
                {
                    power = powerEnum;
                    powerValue = totalPower;
                }
            }

            return power;
        }
        
        public static CharacterPowerEnum RandomFromCommonToPowerful(int availablePower)
        {
            CharacterPowerEnum power = CharacterPowerEnum.common;
            int powerValue = 0;
            foreach (CharacterPowerEnum powerEnum in (CharacterPowerEnum[])System.Enum.GetValues(typeof(CharacterPowerEnum)))
            {
                if (Constants.NotRandomCharacters.Contains(powerEnum))
                {
                    continue;
                }
                int totalPower = GetTotalPower(powerEnum);
                string powerName = powerEnum.ToString();
                CharacterSpawnRateEnum spawnRate = (CharacterSpawnRateEnum)System.Enum.Parse(typeof(CharacterSpawnRateEnum), powerName);
                bool hadLuckyToSpawn = Random.Range(0, 100) < (int)spawnRate;
                if (totalPower <= availablePower && totalPower > powerValue && spawnRate != CharacterSpawnRateEnum.common && hadLuckyToSpawn)
                {
                    power = powerEnum;
                    powerValue = totalPower;
                }
                
            }

            return power;
        }
    }
}