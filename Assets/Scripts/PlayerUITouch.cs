using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Test.TutorialMultiplayer
{
    public class PlayerUITouch : MonoBehaviour
    {
        void Awake() {
            if (Launcher.isTouchscreen) {
                this.gameObject.SetActive(true);
            } else {
                this.gameObject.SetActive(false);
            }
        }
    }
}

