using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Text _text;

    #endregion

    #region Supporting Functions

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name;
    }

    public void OnClick_Button()
    {
        LobbyManager.Instance.JoinRoom(RoomInfo.Name);
    }

    #endregion
}
