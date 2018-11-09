using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos {
    public class Debugger : MonoBehaviour {

        private Text text;

        private void Awake()
        {
            StaticManager.debugger = this;
        }

        private void Start()
        {
            text = GetComponent<Text>();
        }

        public void DegubLog(string log)
        {
            text.text += log;
        }
    }
}