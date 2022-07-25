using LightReflectiveMirror;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

namespace Project
{
    public class StartNetworkIfNotStarted : MonoBehaviour
    {
        [Header("Networking..")] //
        public NetworkManager disabledNetworkManager;
        public LightReflectiveMirrorTransport transport;

        [Header("Player Handling..")] //
        public GameObject disableExistingPlayerInSceneForPlay;

        private bool handleNewNetworkManager;

        void Start()
        {
            if (FindObjectOfType<NetworkManager>() == null)
            {
                disabledNetworkManager.gameObject.SetActive(true);
                handleNewNetworkManager = true;
            }
        }

        void Update()
        {
            if (!disableExistingPlayerInSceneForPlay.IsUnityNull())
                disableExistingPlayerInSceneForPlay.SetActive(false);

            if (handleNewNetworkManager && transport.IsAuthenticated() && transport.Available())
            {
                disabledNetworkManager.StartHost();
                handleNewNetworkManager = false;
            }
        }
    }
}
