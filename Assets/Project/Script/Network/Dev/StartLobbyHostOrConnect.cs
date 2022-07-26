using System;
using System.Linq;
using Base;
using LightReflectiveMirror;
using MyBox;
using Unity.VisualScripting;
using UnityEngine;

namespace Project
{
    public class StartLobbyHostOrConnect : MonoBehaviour
    {
        public LightReflectiveMirrorTransport transport;
        public int numberOfInstancesToStart = 2;

        private bool handleNewNetworkManager = true;

        void Update()
        {
            // Start hosting and or auto connect
            if (handleNewNetworkManager && transport.IsAuthenticated() && transport.Available())
            {
                HandleNetworkManager();
                handleNewNetworkManager = false;
            }

            // Handle ready state
            if (transport.IsAuthenticated() && transport.Available())
            {
                var roomPlayer = MyNetworkManager.singleton.ownRoomPlayer;
                if (!roomPlayer.IsUnityNull() && !roomPlayer.readyToBegin && roomPlayer.authorityStarted)
                    roomPlayer.CmdChangeReadyState(true);
            }
        }

        private void HandleNetworkManager()
        {
            MyNetworkManager.singleton.minPlayers = numberOfInstancesToStart;

            // Handle hosting or connecting
            var devServerName = $"DEV {PlayerPrefs.GetString(PlayerPrefsConstants.Username.GetStringValue())}";

            void Listener()
            {
                var rooms = transport.relayServerList;
                var room = rooms.FirstOrDefault(room1 => room1.serverData.Equals(devServerName));

                Debug.Log($"StartHostOrConnect got {rooms.Count} rooms.");
                Debug.Log($"StartHostOrConnect room with id {room.serverId} and will connect if id is set.");

                if (!room.serverId.IsNullOrEmpty())
                {
                    Debug.Log($"StartHostOrConnect attempting to connect.");
                    MyNetworkManager.singleton.networkAddress = room.serverId;
                    MyNetworkManager.singleton.StartClient();
                }
                else
                {
                    Debug.Log($"StartHostOrConnect attempting to start hosting.");
                    transport.extraServerData = devServerName;
                    MyNetworkManager.singleton.StartHost();
                }
                
                // Remove listener after done
                transport.serverListUpdated.RemoveListener(Listener);
            }

            // Register listener
            transport.serverListUpdated.AddListener(Listener);
        }
    }
}
