using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.Test.TutorialMultiplayer
{
    public class HideAndShowChat : MonoBehaviour
    {
        private RectTransform chatbox;
        private bool isChatHidden;

        void Awake() {
            chatbox = GetComponent<RectTransform>();
            isChatHidden = false;
        }

        public void ChatHider() {
            if (isChatHidden) {
                LeanTween.moveY(chatbox, 0f, 0.25f);
                isChatHidden = false;
            } else {
                LeanTween.moveY(chatbox, 163f, 0.25f);
                isChatHidden = true;
            }   
        }
    }
}

