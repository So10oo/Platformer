using System;
using UnityEngine;

[Serializable]
public class PlayerData : ISaveable
{
    public Vector2 position;

    public string Id { get; set; }
}