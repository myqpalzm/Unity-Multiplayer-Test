using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Test.TutorialMultiplayer
{
    public class ChooseInput : MonoBehaviour
    {
        #region Public Fields



        #endregion

        #region Public Methods

        public void OnChooseKeyboard() {
            Launcher.isTouchscreen = false;
        }

        public void OnChooseTouchscreen() {
            Launcher.isTouchscreen = true;
        }

        #endregion
    }
}

