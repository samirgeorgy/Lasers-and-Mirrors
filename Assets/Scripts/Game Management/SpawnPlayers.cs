using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private float minX, MaxX, minY, MaxY;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, MaxX), Random.Range(minY, MaxY), 0);

        int index = Random.Range(0, 2);

        PhotonNetwork.Instantiate(_playerPrefabs[index].name, randomPosition, Quaternion.identity);
    }
}
