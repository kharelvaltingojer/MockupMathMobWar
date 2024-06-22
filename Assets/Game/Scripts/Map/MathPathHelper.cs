using System;
using UnityEngine;

namespace Game.Scripts.Map
{
    public static class MathPathHelper
    {
        /// <summary>
        /// Get the result of the operation
        /// we have to makke the operation with the (currentSpawns ?? 1) and the operand
        /// if we are dividing we should kill MiniPlayer until The int rounded up division result
        /// if we are multiplying we should spawn MiniPlayer with the difference between the result and the currentSpawns
        /// if we are adding we should linear add the operand as spawn
        ///
        /// Parameters:
        /// <param name="operation">The operation to make</param>
        /// <param name="operand">The operand to make the operation</param>
        /// <param name="spawn">The current spawn of the player</param>
        /// </summary> 
        public static int GetResult(MathPanelRandomOperation.Operation operation, int operand, int spawn)
        {
            int result = 0;
            switch (operation)
            {
                case MathPanelRandomOperation.Operation.Addition:
                    result = operand;
                    break;
                case MathPanelRandomOperation.Operation.Subtraction:
                    result = Mathf.Abs(operand) * -1;
                    break;
                case MathPanelRandomOperation.Operation.Multiplication:
                    var multiplicationResult = Math.Max(spawn, 1) * operand;
                    result = multiplicationResult - spawn;
                    break;
                case MathPanelRandomOperation.Operation.Division:
                    // divide round up
                    float floatSpawn = Math.Max(spawn, 1);
                    var divisionResult = Mathf.Ceil(floatSpawn / operand);
                    var diff = Mathf.Abs(spawn - (int)divisionResult);
                    result = diff * -1;
                    break;
            }

            return result;
        }
    }
}