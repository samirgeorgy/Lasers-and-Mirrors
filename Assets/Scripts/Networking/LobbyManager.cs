using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Private variables

    static private LobbyManager _instance;

    [SerializeField] private InputField createInput;
    [SerializeField] private InputField joinInput;
    [SerializeField] private InputField nicknameInput;

    #endregion

    #region Public Properties

    static public LobbyManager Instance
    {
        get { return _instance; }
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        nicknameInput.text = PlayerPrefs.GetString("nickname", "");
    }

    #endregion

    #region Supporting Functions

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(nicknameInput.text))
        {
            Lobby_UI.Instance.TogglePlayerMsg(true);
            return;
        }
        else
            Lobby_UI.Instance.TogglePlayerMsg(false);

        if (string.IsNullOrEmpty(createInput.text))
        {
            Lobby_UI.Instance.ToggleCreateRoomMsg(true);
            return;
        }
        else
            Lobby_UI.Instance.ToggleCreateRoomMsg(false);

        Lobby_UI.Instance.UpdateConnectionMsg("Creating Room...");
        PlayerPrefs.SetString("nickname", nicknameInput.text);
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom(string roomName)
    {
        if (string.IsNullOrEmpty(nicknameInput.text))
        {
            Lobby_UI.Instance.TogglePlayerMsg(true);
            return;
        }
        else
            Lobby_UI.Instance.TogglePlayerMsg(false);

        Lobby_UI.Instance.UpdateConnectionMsg("Joining Room...");
        PlayerPrefs.SetString("nickname", nicknameInput.text);
        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Lobby_UI.Instance.UpdateConnectionMsg("Joining Room...");
        PhotonNetwork.LoadLevel("Game");
        //StartCoroutine(RoomTransitionRoutine());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator RoomTransitionRoutine()
    {
        Lobby_UI.Instance.FadeOut();

        yield return new WaitForSeconds(1.2f);

        PhotonNetwork.LoadLevel("Game");
    }

    #endregion
}
