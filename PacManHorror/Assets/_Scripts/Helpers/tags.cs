using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerTags
{
    public static string hands = "Hand";
    public static string coin = "coinPurse";

    public static string player = "Player";
}

public static class Axistags
{
    public static string Horizontal = "Horizontal";
    public static string Vertical = "Vertical";
}

public static class Scriptables
{
    public static Scores PlayerScore = (Scores)Resources.Load("Scriptables/PlayerScore");
    public static Scores PlayerMetaScore = (Scores)Resources.Load("Scriptables/PlayerMetaScore");
    public static GameObject teleportationArea = (GameObject)Resources.Load("TeleportationArea");

    public static Upgrades Minimap = (Upgrades)Resources.Load("Scriptables/Upgrades/MiniMap");
    public static Upgrades Magnet = (Upgrades)Resources.Load("Scriptables/Upgrades/Magnet");
    public static Upgrades Stim = (Upgrades)Resources.Load("Scriptables/Upgrades/Stim");

    public static Levels LevelManager = (Levels)Resources.Load("Scriptables/levelManager");

}

public static class MaskTags
{

    public static LayerMask playerMask = LayerMask.GetMask("WhatIsPlayer");
    public static LayerMask groundMask = LayerMask.GetMask("WhatIsGround");
}

public static class AudioTags
{
    public static string CoinPickUp = "CoinPickUp";
}

public static class CameraTags
{
    public static Camera mainCamera = Camera.main;
}