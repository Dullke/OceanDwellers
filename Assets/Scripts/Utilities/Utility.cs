using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Cooldown
    {

        //TODO: Add function tooltip.
        public static bool TryStart(GameObject applyTo, string label, float time)
        {
            if (IsActive(applyTo, label)) { return false; }

            Timer cooldown = applyTo.AddComponent<Timer>();
            cooldown.Time = time;
            cooldown.Label = label;
            return true;
        }

        //TODO: Add function tooltip.
        private static bool IsActive(GameObject appliedTo, string label)
        {
            Timer[] possibleTimers = appliedTo.GetComponents<Timer>();
            if (possibleTimers == null) { return false; }

            for (int i = 0; i < possibleTimers.Length; i++)
            {
                if (possibleTimers[i].Label == label)
                    return true;
            }
            return false;

        }

    }

    public class Misc
    {
        public static Vector2Int RoundUpVector2(Vector2 toRound)
        {
            return new Vector2Int(Mathf.FloorToInt(toRound.x), Mathf.FloorToInt(toRound.y));
        }



    }

}

