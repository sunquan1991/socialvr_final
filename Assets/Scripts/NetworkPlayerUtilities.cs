﻿using System;
using UnityEngine;

public class NetworkPlayerUtilities : MonoBehaviour
{
    public static string[] PlayerTypes()
    {
        string[] playerTypes = { "Cat", "Human", "Human", "God" };
        return playerTypes;
    }

    public static string LocalPlayerType()
    {
        NetworkPlayer localPlayer = new NetworkPlayer();
        foreach (NetworkPlayer player in Players())
        {
            if (player.isLocalPlayer)
            {
                localPlayer = player;
                break;
            }
        }
        return PlayerType(localPlayer.gameObject);
    }

    public static int PlayerIndex(GameObject player) {
        return Array.IndexOf(Players(), player.GetComponent<NetworkPlayer>());
    }

    public static string PlayerType(GameObject player)
    {
        return PlayerTypes()[PlayerIndex(player)];    
    }

    public static GameObject PlayerCenterEyeAnchor(GameObject player)
    {
        Transform wrapper = player.transform.Find(PlayerType(player));
        Transform centerEyeAnchorTransform = wrapper.Find("OVRCameraRig").Find("TrackingSpace").Find("CenterEyeAnchor");
        return centerEyeAnchorTransform.gameObject;
    }

    private static NetworkPlayer[] Players()
    {

        NetworkPlayer[] players = FindObjectsOfType<NetworkPlayer>();
        Array.Sort(players, CompareNetId);
        return players;
    }

    private static int CompareNetId(NetworkPlayer a, NetworkPlayer b)
    {
        return a.netId.CompareTo(b.netId);
    }
}
