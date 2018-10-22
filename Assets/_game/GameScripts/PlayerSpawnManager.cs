using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mangos
{
    public class PlayerSpawnManager : MonoBehaviour
    {
        static PlayerController player1;
        static PlayerController player2;
        static List<PlayerController>  spectators = new List<PlayerController>();

        static Vector3 p1StartPos = new Vector3(-8.5f, 0, 0);
        static Vector3 p2StartPos = new Vector3(8.5f, 0, 0);

        static public GameManager gameManager;

        static public void SetMeUp(PlayerController newPlayer)
        {
            if (player1 == null)
            {
                player1 = newPlayer;
                newPlayer.transform.position = p1StartPos;
                Debug.Log("Player 1 connected, pos: " + newPlayer.transform.position);
            }
            else if (player2 == null)
            {
                player2 = newPlayer;
                player2.transform.position = p2StartPos;
                Debug.Log("Player 2 connected, pos: " + newPlayer.transform.position);
                gameManager.StartGame();
            }
            else
            {
                spectators.Add(newPlayer);
                newPlayer.gameObject.SetActive(false);
            }

        }
    }
}