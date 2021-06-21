using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Private Variables

    static private Target _instance;

    [SerializeField] private Sprite _greenBoard;
    [SerializeField] private Sprite _redBoard;
    [SerializeField] private SpriteRenderer _renderer;

    #endregion

    #region Public Properties

    static public Target Instance
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
        _renderer.sprite = _redBoard;
    }

    #endregion

    #region Supporting Functions

    public void SwitchBoardToGreen()
    {
        _renderer.sprite = _greenBoard;
    }    

    #endregion
}
