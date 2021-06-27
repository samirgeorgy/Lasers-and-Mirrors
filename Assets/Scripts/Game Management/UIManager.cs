using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Private Variables

    static private UIManager _instance;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _roomName;
    [SerializeField] private Animator _fadeAnim;
    [SerializeField] private GameObject _tutorialWindow;

    #endregion

    #region Public Properties

    static public UIManager Instance
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
        int fisrtTime = PlayerPrefs.GetInt("FirstTimePlay", 0);

        if (fisrtTime == 0)
        {
            _tutorialWindow.SetActive(true);
            PlayerPrefs.SetInt("FirstTimePlay", 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
    }

    #endregion

    #region Supporting Functions

    public void EnableWinScreen()
    {
        _winPanel.SetActive(true);
    }

    public void UpdatePlayerName(string name)
    {
        _playerName.text = name;
    }

    public void UpdateRoomName(string name)
    {
        _roomName.text = "Room: " + name;
    }

    public void FadeOut()
    {
        _fadeAnim.SetTrigger("FadeOut");
    }

    #endregion
}
