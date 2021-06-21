using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    #region Private Variables

    static private MainMenu_UI _instance;

    [SerializeField] private Text _connectionMsgTxt;
    [SerializeField] private Animator _fadeAnim;

    #endregion

    #region Public Properties

    static public MainMenu_UI Instance
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

    public void UpdateConnectionText(string msg)
    {
        _connectionMsgTxt.text = msg;
    }

    public void FadeOut()
    {
        _fadeAnim.SetTrigger("FadeOut");
    }

    #endregion
}
