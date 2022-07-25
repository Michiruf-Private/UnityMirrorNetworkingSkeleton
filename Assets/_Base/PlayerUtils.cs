using System;
using UnityEngine;

namespace Base
{
    public static class PlayerUtils
    {
        private const string PlayerTag = UnityTag.Player;

        public static Transform FindFirstPlayer(bool throwException = true)
        {
            var targetObject = GameObject.FindGameObjectWithTag(PlayerTag);
            if (!targetObject)
            {
                if (throwException)
                    throw new ArgumentException($"Cannot find target by \"{PlayerTag}\" tag!");
                return null;
            }

            return targetObject.transform;
        }
    }
}
