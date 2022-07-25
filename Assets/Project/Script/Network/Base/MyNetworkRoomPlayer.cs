using Base;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
    public class MyNetworkRoomPlayer : NetworkRoomPlayer
    {
        [SyncVar] public string username;

        public UnityEvent onStartAuthority;
        public UnityEvent<bool> onReadyStateChanged;

        public override void OnStartAuthority()
        {
            CmdSetUsername(PlayerPrefs.GetString(PlayerPrefsConstants.Username.GetStringValue()));
            onStartAuthority?.Invoke();
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            onReadyStateChanged?.Invoke(newReadyState);
        }

        [Command]
        private void CmdSetUsername(string username)
        {
            this.username = username;
        }
    }
}
