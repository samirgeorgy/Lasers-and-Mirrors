using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private float _laserStartWidth = 0.1f;
    [SerializeField] private float _laserEndWidth = 0.1f;
    [SerializeField] private Color _laserStartColor;
    [SerializeField] private Color _laserEndColor;
    [SerializeField] private Material _material;

    private LaserBeam _beam;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_beam != null)
            Destroy(_beam.laserObj);

        _beam = new LaserBeam(transform.position, transform.up, _laserStartWidth, _laserEndWidth, _laserStartColor, _laserEndColor, _material);
    }

    #endregion
}
