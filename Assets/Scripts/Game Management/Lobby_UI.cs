using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : MonoBehaviour
{
    #region Private Variables

    private static Lobby_UI _instance;

    [SerializeField] private Text _connectionMsg;
    [SerializeField] private Animator _fadeAnim;
    [SerializeField] private GameObject _playerNameMsg;
    [SerializeField] private GameObject _createRoomMsg;

    #endregion

    #region Public Properties

    public static Lobby_UI Instance
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Supporting Functions

    public void UpdateConnectionMsg(string msg)
    {
        _connectionMsg.text = msg;
    }

    public void FadeOut()
    {
        _fadeAnim.SetTrigger("FadeOut");
    }

    public void TogglePlayerMsg(bool active)
    {
        _playerNameMsg.SetActive(active);
    }

    public void ToggleCreateRoomMsg(bool active)
    {
        _createRoomMsg.SetActive(active);
    }

    #endregion
}
