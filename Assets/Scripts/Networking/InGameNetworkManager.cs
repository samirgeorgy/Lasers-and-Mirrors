using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class InGameNetworkManager : MonoBehaviourPunCallbacks
{
    #region Unity Functions

    private void Start()
    {
        UIManager.Instance.UpdatePlayerName(PhotonNetwork.LocalPlayer.NickName);
        UIManager.Instance.UpdateRoomName(PhotonNetwork.CurrentRoom.Name);
    }

    #endregion

    #region Supporting Functions

    public void DisconnectPlayer()
    {
        StartCoroutine(ConnectToLobby());
    }

    IEnumerator ConnectToLobby()
    {
        UIManager.Instance.FadeOut();

        yield return new WaitForSeconds(1.1f);

        PhotonNetwork.LeaveRoom();

        while (PhotonNetwork.InRoom)
            yield return null;

        SceneManager.LoadScene(1);
    }

    #endregion
}
