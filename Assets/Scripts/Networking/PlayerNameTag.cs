using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class PlayerNameTag : MonoBehaviourPun
{
    #region Private Variables

    [SerializeField] private Text _playerNameText;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
            return;

        SetNickname();
    }

    #endregion

    #region Supporting Functions

    private void SetNickname()
    {
        _playerNameText.text = photonView.Owner.NickName;
    }

    #endregion
}
