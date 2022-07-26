using Mirror;

namespace Project
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
        [SyncVar]
        internal MyNetworkRoomPlayer roomPlayer;
    }
}
