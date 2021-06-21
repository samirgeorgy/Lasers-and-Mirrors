using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveObject : MonoBehaviour
{
    #region Private Variables

    private Vector3 mOffset;

    PhotonView _view;

    #endregion

    #region Unity Functions

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnMouseDown()
    {
        if (_view.IsMine)
        {
            if (GameManager.Instance.GameWon == false)
                mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        if (_view.IsMine)
        {
            if (GameManager.Instance.GameWon == false)
                transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    #endregion

    #region Supporting Functions

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    #endregion
}
