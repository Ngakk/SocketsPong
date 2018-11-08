using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Mangos
{
    public class GameManager : NetworkBehaviour
    {
        public FloatVariable p1Score, p2Score;
        public GameEvent gameStart;
        
        private void Awake()
        {
            StaticManager.gameManager = this;
        }

        // Use this for initialization
        void Start()
        {
            p1Score.value = 0;
            p2Score.value = 0;
        }

        [ClientRpc]
        public void RpcStartGame()
        {
            StartGame();
        }

        public void StartGame()
        {
            gameStart.Raise();
            Debug.Log("Started Game");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LogInt(int var)
        {
            Debug.Log(var);
        }

        public void OnScore(int player)
        {
            if (player == 0)
            {
                p1Score.value++;
            }
            else
            {
                p2Score.value++;
            }
        }
    }
}
