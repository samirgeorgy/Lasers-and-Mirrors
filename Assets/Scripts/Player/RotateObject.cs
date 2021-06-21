using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class RotateObject : MonoBehaviour
{
    #region Private Veriables

    private Camera _cam;
    private Vector3 _screenPos;
    private float _angleOffset;

    PhotonView _view;

    #endregion

    #region Unity Functions

    private void Start()
    {
        _cam = Camera.main;
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_view.IsMine)
        {
            if (GameManager.Instance.GameWon == false)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _screenPos = _cam.WorldToScreenPoint(transform.position);
                    Vector3 v3 = Input.mousePosition - _screenPos;
                    _angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
                }

                if (Input.GetMouseButton(1))
                {
                    Vector3 v3 = Input.mousePosition - _screenPos;
                    float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle + _angleOffset);
                }
            }
        }
    }

    #endregion
}
