using System.Collections.Generic;
using Base;
using LightReflectiveMirror;
using TMPro;
using UnityEngine;

namespace Project
{
    public class ServerHandler : MonoBehaviour
    {
        [Header("Preferences..")] //
        public float fetchListInterval = 5f;

        [Header("References..")] //
        public LightReflectiveMirrorTransport transport;
        public RectTransform serverListContainer;
        public GameObject serverListRowPrefab;
        public TMP_InputField usernameInput;

        private float nextTimeToFetch;

        void Start()
        {
            transport.serverListUpdated.AddListener(() => DrawServerList(transport.relayServerList));
        }

        void Update()
        {
            if (transport.IsAuthenticated() && transport.Available() && Time.realtimeSinceStartup >= nextTimeToFetch)
            {
                nextTimeToFetch = Time.realtimeSinceStartup + fetchListInterval;
                transport.RequestServerList();
            }
        }

        private void DrawServerList(List<Room> rooms)
        {
            serverListContainer.RemoveAllChildren();
            foreach (var room in rooms)
            {
                var o = Instantiate(serverListRowPrefab, serverListContainer);
                var r = o.GetComponent<ServerListRow>();
                r.ApplyRoom(room);
            }
        }

        public void StartHost(bool serverOnly)
        {
            if (string.IsNullOrEmpty(usernameInput.text))
                return;

            transport.serverName = usernameInput.text;
            
            // Cancel any currently started servers, to be able to try again
            MyNetworkManager.singleton.StopHost();

            if (serverOnly)
                MyNetworkManager.singleton.StartServer();
            else
                MyNetworkManager.singleton.StartHost();
        }
    }
}
