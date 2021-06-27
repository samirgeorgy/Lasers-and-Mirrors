using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Private Variables

    static private NetworkManager _instance;

    private List<RoomInfo> _roomList;

    #endregion

    #region Public Properties

    static public NetworkManager Instance
    {
        get { return _instance; }
    }

    public List<RoomInfo> RoomList
    {
        get { return _roomList; }
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _roomList = new List<RoomInfo>();

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

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
    }

    #endregion
}
