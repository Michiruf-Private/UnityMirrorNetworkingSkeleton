using System;
using LightReflectiveMirror;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ServerListRow : MonoBehaviour
    {
        public TMP_Text serverName;
        public TMP_Text serverId;
        public TMP_Text players;
        public Button joinButton;

        [Header("Preferences..")] //
        public float joinButtonTimeout = 5f;

        private static float nextJoinAllowed;

        void Update()
        {
            joinButton.interactable = Time.realtimeSinceStartup >= nextJoinAllowed;
        }

        public void ApplyRoom(Room room)
        {
            serverName.text = room.serverName;
            serverId.text = room.serverId;
            players.text = $"{room.currentPlayers} / {room.maxPlayers}";

            joinButton.onClick.AddListener(() =>
            {
                if (Time.realtimeSinceStartup < nextJoinAllowed)
                    return;
                
                nextJoinAllowed = Time.realtimeSinceStartup + joinButtonTimeout;
                
                MyNetworkManager.singleton.networkAddress = room.serverId;
                MyNetworkManager.singleton.StartClient();
            });
        }
    }
}
