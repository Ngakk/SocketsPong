using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    [RequireComponent(typeof(Text))]
    public class TextChanger : MonoBehaviour
    {
        public FloatVariable score;
        Text text;

        // Use this for initialization
        void Start()
        {
            text = GetComponent<Text>();
            text.text = "0";
        }

        public void UpdateText()
        {
            text.text = score.value.ToString();
        }
    }
}