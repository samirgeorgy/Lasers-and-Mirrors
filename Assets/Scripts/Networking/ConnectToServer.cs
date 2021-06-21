using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    #endregion

    #region Supporting Functions

    public override void OnConnectedToMaster()
    {
        MainMenu_UI.Instance.UpdateConnectionText("Connected...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        StartCoroutine(TransitionToLobbyRoutine());
    }

    IEnumerator TransitionToLobbyRoutine()
    {
        MainMenu_UI.Instance.FadeOut();

        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene(1);
    }

    #endregion
}
