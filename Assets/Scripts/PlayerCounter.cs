using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerCounter : MonoBehaviour
{
    private string playerCount;
    private TextMeshProUGUI counter;

    void Awake() {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        counter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        counter.text = playerCount;
    }
}
