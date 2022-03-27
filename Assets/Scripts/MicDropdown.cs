using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.Test.TutorialMultiplayer
{
    public class MicDropdown : MonoBehaviour
    {
        #region Private Fields

        //private List<string> mics = new List<string>();

        #endregion

        #region Public Fields

        public TMP_Dropdown dropdown;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            dropdown.options.Clear();
            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
                dropdown.options.Add(new TMP_Dropdown.OptionData() {text = device});
            }
        }

        #endregion
    }
}

