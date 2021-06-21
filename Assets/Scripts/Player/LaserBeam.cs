using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    #region Private Variables

    public GameObject laserObj;
    private LineRenderer _laser;
    private List<Vector3> _laserIndices = new List<Vector3>();

    private Dictionary<string, float> refractiveMat = new Dictionary<string, float>()
    {
        {"Air", 1.0f },
        {"Glass", 1.5f }
    };

    private Dictionary<int, string> usedObjects = new Dictionary<int, string>();

    #endregion

    #region Constructor

    public LaserBeam (Vector3 pos, Vector3 dir, float startWidth, float endWidth, Color startColor, Color endColor, Material material)
    {
        _laser = new LineRenderer();
        laserObj = new GameObject();
        laserObj.name = "Laser Beam";

        _laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        _laser.startWidth = startWidth;
        _laser.endWidth = endWidth;
        _laser.startColor = new Color(startColor.r, startColor.g, startColor.b, 100);
        _laser.endColor = new Color(endColor.r, endColor.g, endColor.b, 100);
        _laser.material = material;
        _laser.textureMode = LineTextureMode.Tile;
        CastLaser(pos, dir);
    }

    #endregion

    #region Member Functions

    private void CastLaser(Vector3 pos, Vector3 dir)
    {
        _laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, 1))
        {
            CheckHit(hit, dir);
        }
        else
        {
            _laserIndices.Add(ray.GetPoint(100));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit hit, Vector3 direction)
    {
        if (hit.collider.tag.Equals("Mirror"))
        {
            try
            {
                usedObjects.Add(hit.collider.gameObject.GetInstanceID(), "Mirror");
            }
            catch { }

            Vector3 pos = hit.point;
            Vector3 dir = Vector3.Reflect(direction, hit.normal);

            CastLaser(pos, dir);
        }
        else if (hit.collider.tag.Equals("Lense"))
        {
            try
            {
                usedObjects.Add(hit.collider.gameObject.GetInstanceID(), "Lense");
            }
            catch { }

            Vector3 pos = hit.point;
            _laserIndices.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.001f + pos.x,
                                          Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.001f + pos.y,
                                          Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.001f + pos.z);

            float n1 = refractiveMat["Air"];
            float n2 = refractiveMat["Glass"];

            Vector3 norm = hit.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);

            CastLaser(newPos1, refractedVector);

            /*Ray ray1 = new Ray(newPos1, refractedVector);
            Vector2 newRayStartPos = ray1.GetPoint(1.5f);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 1.6f, 1))
            {
                _laserIndices.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);

            CastLaser(hit2.point, refractedVector2);*/
        }
        else if (hit.collider.tag.Equals("Target"))
        {
            _laserIndices.Add(hit.point);
            UpdateLaser();

            //Count the number of used objects and if thats the case the player wins
            GameManager.Instance.CheckWin(usedObjects.Count);
        }
        else
        {
            _laserIndices.Add(hit.point);
            UpdateLaser();
        }
    }

    private Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        incident.Normalize();

        Vector3 refractedVector = (n1 / n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1 / n2 * n1 / n2), Vector3.Cross(norm, incident)))).normalized;
        return refractedVector;
    }

    private void UpdateLaser()
    {
        int count = 0;
        _laser.positionCount = _laserIndices.Count;

        foreach (Vector3 index in _laserIndices)
        {
            _laser.SetPosition(count, index);
            count++;
        }
    }

    #endregion
}
