using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Player
{
    public static event Action ClientOnInfoUpdated;

    [SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))] string displayName;
    public string PlayerDisplayName
    {
        get { return displayName; }
        [Server]
        set { displayName = value; }                      
    }

    void ClientHandleDisplayNameUpdated(string oldName, string newName)
    {
        ClientOnInfoUpdated?.Invoke();
    }
}
