using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private float _defaultDistance = 100f;
    [SerializeField] private Transform _laserFirePoint;
    [SerializeField] private LineRenderer _lineRenderer;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    #endregion

    #region Supporting Functions

    private void ShootLaser()
    {
        if (Physics2D.Raycast(transform.position, transform.up))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up);

            DrawRay(_laserFirePoint.position, hitInfo.point);
        }
        else
        {
            DrawRay(_laserFirePoint.position, _laserFirePoint.transform.up * _defaultDistance);
        }
    }

    private void DrawRay (Vector2 startPos, Vector2 endPos)
    {
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }

    #endregion
}
