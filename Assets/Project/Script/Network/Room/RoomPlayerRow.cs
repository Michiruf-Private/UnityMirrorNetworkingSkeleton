using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class RoomPlayerRow : MonoBehaviour
    {
        public TMP_Text username;
        public Button kickButton;
        public GameObject readyContainer;
        public GameObject unreadyContainer;

        public void ApplyRoomPlayer(MyNetworkRoomPlayer player)
        {
            var isServer = MyNetworkManager.singleton.ownRoomPlayer.isServer;
            var showKickButton = isServer && !player.isLocalPlayer;
            
            username.text = player.username;

            kickButton.gameObject.SetActive(showKickButton);
            kickButton.onClick.AddListener(() =>
            {
                // TODO Kicking does not work if we use the LightReflectiveMirrorTransport
                //      Maybe because it kills the connection to the relay server instead?
                Debug.LogWarning("When using LRM kicking will not work properly");
                player.netIdentity.connectionToClient.Disconnect();
            });

            readyContainer.SetActive(player.readyToBegin);
            unreadyContainer.SetActive(!player.readyToBegin);
            // NOTE This is actually not needed, because we erase the whole list once in a time
            // player.onReadyStateChanged.AddListener(ready =>
            // {
            //     readyContainer.SetActive(ready);
            //     unreadyContainer.SetActive(!ready);
            // });
        }
    }
}
