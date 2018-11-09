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

            if (NetworkServer.connections.Count == 2)
                Invoke("StartGame", 1);
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);
            Debug.Log("server ready: " + NetworkServer.connections.Count);
        }

        public override void OnClientConnect(NetworkConnection connection)
        {
            ClientScene.Ready(connection);
            ClientScene.AddPlayer(0);

            //Output text to show the connection on the client side
            Debug.Log("Client Side : Client " + connection.connectionId + " Connected!");
            if(connection.connectionId == 1)
                Invoke("StartGame", 1);
            //Register and receive the message on the Client's side (NetworkConnection.Send Example)
            //client.RegisterHandler(MsgType.Ready, ReadyMessage);
            Debug.Log("client connect: " + NetworkServer.connections.Count);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            NetworkServer.SetClientReady(conn);
            Debug.Log("Someone disconnected: " + NetworkServer.connections.Count);
        }
    }
}