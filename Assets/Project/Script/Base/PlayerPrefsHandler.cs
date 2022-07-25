using System;
using Base;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
    public class PlayerPrefsHandler : MonoBehaviour
    {
        public PlayerPrefsConstants key;
        public Type type = Type.String;

        public UnityEvent<string> onStartString;
        public UnityEvent<int> onStartInt;
        public UnityEvent<float> onStartFloat;

        void Start()
        {
            switch (type)
            {
                case Type.String:
                    onStartString?.Invoke(PlayerPrefs.GetString(key.GetStringValue()));
                    break;
                case Type.Int:
                    onStartInt?.Invoke(PlayerPrefs.GetInt(key.GetStringValue()));
                    break;
                case Type.Float:
                    onStartFloat?.Invoke(PlayerPrefs.GetFloat(key.GetStringValue()));
                    break;
            }
        }

        [UsedImplicitly]
        public void SetString(string value)
        {
            if (type != Type.String)
                throw new ArgumentException("PlayerPrefsHandler for ${key} is set to a different type than string.");
            PlayerPrefs.SetString(key.GetStringValue(), value);
        }

        [UsedImplicitly]
        public void SetInt(int value)
        {
            if (type != Type.Int)
                throw new ArgumentException("PlayerPrefsHandler for ${key} is set to a different type than string.");
            PlayerPrefs.SetInt(key.GetStringValue(), value);
        }

        [UsedImplicitly]
        public void SetFloat(float value)
        {
            if (type != Type.Float)
                throw new ArgumentException("PlayerPrefsHandler for ${key} is set to a different type than string.");
            PlayerPrefs.SetFloat(key.GetStringValue(), value);
        }

        public enum Type
        {
            String = 0,
            Int = 1,
            Float = 2
        }
    }
}
