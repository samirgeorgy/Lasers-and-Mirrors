using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPun, IPunObservable
{
    #region Private Variables

    static private GameManager _instance;

    [SerializeField] private Transform[] _locations;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _targetPrefab;

    private int laserPosIndex = 0;
    private int targetPosIndex = 0;
    private bool _gameWon = false;

    #endregion

    #region Public Properties

    static public GameManager Instance
    {
        get { return _instance; }
    }

    public bool GameWon
    {
        get { return _gameWon; }
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
        if (PhotonNetwork.IsMasterClient)
        {
            laserPosIndex = Random.Range(0, _locations.Length);
            targetPosIndex = Random.Range(0, _locations.Length);

            while (laserPosIndex == targetPosIndex)
                targetPosIndex = Random.Range(0, _locations.Length);

            Transform laserPos = _locations[laserPosIndex];
            Transform targetPos = _locations[targetPosIndex];

            Instantiate(_laserPrefab, laserPos);
            Instantiate(_targetPrefab, targetPos);
        }
    }

    #endregion

    #region Supporting Functions

    public void CheckWin(int objectsUsed)
    {
        int totalObjectsInScene = GameObject.FindGameObjectsWithTag("Mirror").Length + GameObject.FindGameObjectsWithTag("Lense").Length;

        if (totalObjectsInScene == objectsUsed)
        {
            Target.Instance.SwitchBoardToGreen();
            UIManager.Instance.EnableWinScreen();
            _gameWon = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Vector2 indices = new Vector2(laserPosIndex, targetPosIndex);
            stream.Serialize(ref indices);
        }
        else if (stream.IsReading)
        {
            Vector2 indices = Vector2.zero;
            stream.Serialize(ref indices);

            Transform laserPos = _locations[(int)indices.x];
            Transform targetPos = _locations[(int)indices.y];

            Instantiate(_laserPrefab, laserPos);
            Instantiate(_targetPrefab, targetPos);
        }
    }

    #endregion
}
