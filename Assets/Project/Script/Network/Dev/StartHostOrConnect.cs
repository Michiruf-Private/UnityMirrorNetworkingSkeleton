using System;
using Mirror;
using UnityEngine;

namespace Project
{
    public class StartHostOrConnect : MonoBehaviour
    {
        private const string PlayerTimeKey = "StartHostOrConnect.Time";

        public GameObject networkManager;

        private static int currentTime => (int)(DateTime.Now.ToFileTimeUtc() / 10000);

        void Start()
        {
            if (FindObjectOfType<NetworkManager>(false) != null)
                return;

            networkManager.gameObject.SetActive(true);
            
            var durationSinceLastTime = currentTime - PlayerPrefs.GetInt(PlayerTimeKey, 0);
            if (durationSinceLastTime > 3)
                NetworkManager.singleton.StartHost();
            else
                NetworkManager.singleton.StartClient();
        }

        void Update()
        {
            if (NetworkServer.active)
                PlayerPrefs.SetInt(PlayerTimeKey, currentTime);
        }
    }
}
