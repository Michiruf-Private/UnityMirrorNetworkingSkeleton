using Base;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Project
{
    public class MyNetworkRoomPlayer : NetworkRoomPlayer
    {
        [SyncVar] public string username;

        internal MyNetworkPlayer player;

        public bool authorityStarted;
        public UnityEvent onStartAuthority;

        public override void OnStartAuthority()
        {
            CmdSetUsername(PlayerPrefs.GetString(PlayerPrefsConstants.Username.GetStringValue()));
            authorityStarted = true;
            onStartAuthority?.Invoke();
        }

        [Command(requiresAuthority = true)]
        private void CmdSetUsername(string username)
        {
            this.username = username;
        }
    }
}
