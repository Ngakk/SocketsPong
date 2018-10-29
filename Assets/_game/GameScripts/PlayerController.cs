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

        private void Awake()
        {
            PlayerSpawnManager.SetMeUp(this);
        }

        public override void OnStartLocalPlayer()
        {
            var comps = GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < comps.Length; i++)
            {
                comps[i].material.color = Color.blue;
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (!isLocalPlayer)
                return;

            if (Input.GetKey(up))
            {
                transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
            }


            if (Input.GetKey(down))
            {
                transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            }

        }
    }
}