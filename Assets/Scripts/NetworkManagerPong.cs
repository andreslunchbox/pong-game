using Mirror;
using UnityEngine;

public class NetworkManagerPong : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    public GameObject ballPrefab;
    public GameObject ball;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
            ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.Spawn(ball);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (ball != null)
        {
            NetworkServer.Destroy(ball);
        }
        base.OnServerDisconnect(conn);
    }
}
