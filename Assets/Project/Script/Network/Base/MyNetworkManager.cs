using System.Collections.Generic;
using System.Linq;
using kcp2k;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
    public class MyNetworkManager : NetworkRoomManager
    {
        public new static MyNetworkManager singleton => NetworkManager.singleton as MyNetworkManager;

        public List<MyNetworkRoomPlayer> roomPlayers => roomSlots.Select(player => player as MyNetworkRoomPlayer).ToList();

        private MyNetworkRoomPlayer ownRoomPlayerInternal => roomSlots.FirstOrDefault(player => player.hasAuthority) as MyNetworkRoomPlayer;
        private MyNetworkRoomPlayer ownRoomPlayerInternalCache;
        public MyNetworkRoomPlayer ownRoomPlayer => ownRoomPlayerInternalCache == null
            ? ownRoomPlayerInternalCache = ownRoomPlayerInternal
            : ownRoomPlayerInternalCache;

        [Header("My Events..")] [Space] //
        public UnityEvent onClientStart;
        public UnityEvent onClientStop;
        public UnityEvent onClientDisconnect;

        public override void OnRoomStartClient()
        {
            onClientStart?.Invoke();
        }

        public override void OnRoomStopClient()
        {
            onClientStop?.Invoke();
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            onClientDisconnect?.Invoke();
        }
    }
}
