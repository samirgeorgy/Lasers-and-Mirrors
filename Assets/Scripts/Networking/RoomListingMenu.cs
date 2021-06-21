using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    #region Private Variables

    [SerializeField] private Transform _content;
    [SerializeField] private RoomListing _roomListingPrefab;

    private List<RoomListing> _listing = new List<RoomListing>();

    #endregion

    #region Supporting Functions

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _listing.Clear();

        foreach (Transform child in _content)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);

                if (index != -1)
                {
                    Destroy(_listing[index].gameObject);
                    _listing.RemoveAt(index);
                }
            }
            else
            {
                RoomListing listing = (RoomListing)Instantiate(_roomListingPrefab, _content);

                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    _listing.Add(listing);
                }
            }
        }
    }

    #endregion
}
