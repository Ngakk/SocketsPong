using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Mangos
{
    public class PlayerSpawnManager : NetworkManager
    {
        private PlayerController player1;
        private PlayerController player2;
        private List<PlayerController> spectators = new List<PlayerController>();

        private Vector3 p1StartPos = new Vector3(-8.5f, 0, 0);
        private Vector3 p2StartPos = new Vector3(8.5f, 0, 0);

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Vector3 posTo;
            if (numPlayers == 0)
                posTo = p1StartPos;
            else if (numPlayers == 1)
                posTo = p2StartPos;
            else
                return;
            GameObject player = (GameObject)Instantiate(playerPrefab, posTo, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);
            Debug.Log("server ready: " + NetworkServer.connections.Count);
            if (NetworkServer.connections.Count == 2)
                Invoke("StartGame", 1);
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            Debug.Log("client connect: " + NetworkServer.connections.Count);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            NetworkServer.SetClientReady(conn);
            Debug.Log("Someone disconnected: " + NetworkServer.connections.Count);
        }

        private void StartGame()
        {
            StaticManager.gameManager.CmdStartGame();
        }
    }
}