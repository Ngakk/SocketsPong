using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Mangos
{
    public class PlayerController : NetworkBehaviour
    {
        public float MoveSpeed = 5f;
        public float yLimits;
        public KeyCode up;
        public KeyCode down;

        private Rigidbody2D rigi;


        public override void OnStartLocalPlayer()
        {
            rigi = GetComponent<Rigidbody2D>();
            var comps = GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < comps.Length; i++)
            {
                comps[i].material.color = Color.blue;
            }
            Debug.Log("");
            if (NetworkServer.connections.Count == 2 && base.isServer)
                Invoke("StartGame", 1);
        }

        // Update is called once per frame
        void Update()
        {

            if (!isLocalPlayer)
                return;

            if (Input.GetKey(up))
            {
                rigi.velocity = Vector3.up * MoveSpeed;
            }
            else if (Input.GetKey(down))
            {
                rigi.velocity = Vector3.down * MoveSpeed;
            }
            else
            {
                rigi.velocity = Vector3.zero;
            }
        }

        private void StartGame()
        {
            StaticManager.gameManager.RpcStartGame();
        }

    }
}