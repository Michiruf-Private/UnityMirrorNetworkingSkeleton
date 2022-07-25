using Base;
using Unity.VisualScripting;
using UnityEngine;

namespace Project
{
    public class RoomHandler : MonoBehaviour
    {
        [Header("Preferences..")] //
        public float redrawListInterval = 1f;

        [Header("References..")] //
        public RectTransform playerListContainer;
        public GameObject playerListRowPrefab;
        public GameObject readyButton;
        public GameObject unreadyButton;

        private float nextTimeToRedraw;
        private static bool isReady => !MyNetworkManager.singleton.ownRoomPlayer.IsUnityNull() &&
                                       MyNetworkManager.singleton.ownRoomPlayer.readyToBegin;

        void Update()
        {
            readyButton.SetActive(isReady);
            unreadyButton.SetActive(!isReady);

            if (Time.realtimeSinceStartup >= nextTimeToRedraw)
            {
                nextTimeToRedraw = Time.realtimeSinceStartup + redrawListInterval;
                DrawPlayerList();
            }
        }

        public void GoBack()
        {
            // Just use stop host, because it is stopping client and server
            // Navigation should occur automatic
            MyNetworkManager.singleton.StopHost();
        }

        public void SetReady(bool ready)
        {
            MyNetworkManager.singleton.ownRoomPlayer.CmdChangeReadyState(ready);
        }

        private void DrawPlayerList()
        {
            playerListContainer.RemoveAllChildren();
            foreach (var roomPlayer in MyNetworkManager.singleton.roomPlayers)
            {
                var o = Instantiate(playerListRowPrefab, playerListContainer);
                var r = o.GetComponent<RoomPlayerRow>();
                r.ApplyRoomPlayer(roomPlayer);
            }
        }
    }
}
