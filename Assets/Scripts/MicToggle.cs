#if !UNITY_WEBGL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
    public class MicToggle : MonoBehaviour
    {
        #region Private Fields

        private Toggle toggleButton;
        private Recorder recorder;

        #endregion
        
        #region Public Fields

        public GameObject voiceManager;

        #endregion

        #region MonoBehaviour Callbacks

        void Awake() {
            recorder = voiceManager.GetComponent<Recorder>();
            recorder.TransmitEnabled = false;
        }

        #endregion

        #region Public Methods

        public void EnableMic(bool toggle) {
            recorder.TransmitEnabled = toggle;
            Debug.Log("Mic enabled: " + toggle);
        }

        #endregion
    }
}

#endif

