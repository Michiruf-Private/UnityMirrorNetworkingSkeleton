using LightReflectiveMirror;
using UnityEngine;

namespace Project
{
    // NOTE If there persists a lobby or in fact a server creation error, there might be an issue with the
    // server itself. Because after restarting, everything works like expected, where it didn't in almost
    // nearly every case tried at the end.
    
    public class LRMDebug : MonoBehaviour
    {
        public bool debugEventsEnabled;
        public bool debugUpdateEnabled;
        public LightReflectiveMirrorTransport lrmTransport;

        void Start()
        {
            if (!debugEventsEnabled)
                return;

            lrmTransport.OnServerConnected += v => Debug.LogWarning("OnServerConnected");
            lrmTransport.OnServerDisconnected += v => Debug.LogWarning("OnServerDisconnected");
            lrmTransport.OnServerError += (v, e) => Debug.LogError("OnServerError");
            lrmTransport.OnServerDataReceived += (v1, v2, v3) => Debug.Log("OnServerDataReceived");
            lrmTransport.OnServerDataSent += (v1, v2, v3) => Debug.Log("OnServerDataSent");

            lrmTransport.OnClientConnected += () => Debug.LogWarning("OnClientConnected");
            lrmTransport.OnClientDisconnected += () => Debug.LogWarning("OnClientDisconnected");
            lrmTransport.OnClientError += e => Debug.LogError("OnClientError");
            lrmTransport.OnClientDataReceived += (v1, v2) => Debug.Log("OnClientDataReceived");
            lrmTransport.OnClientDataSent += (v1, v2) => Debug.Log("OnClientDataSent");
            
            lrmTransport.connectedToRelay.AddListener(() => Debug.LogWarning("ConnectedToRelay"));
            lrmTransport.disconnectedFromRelay.AddListener(() => Debug.LogWarning("DisconnectedFromRelay"));
            lrmTransport.serverListUpdated.AddListener(() => Debug.LogWarning("ServerListUpdated"));

            // This replaces the handlers currently set, so we cant do this
            // NetworkServer.RegisterHandler<ReadyMessage>((client, message) => Debug.Log("ReadyMessage"));
            // NetworkServer.RegisterHandler<CommandMessage>((client, message) => Debug.Log("CommandMessage"));
            // NetworkServer.RegisterHandler<NetworkPingMessage>((client, message) => Debug.Log("NetworkPingMessage"));
        }

        void Update()
        {
            if (!debugUpdateEnabled)
                return;

            Debug.Log(lrmTransport.serverStatus);
        }
    }
}
