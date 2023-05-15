using Leopotam.EcsLite;
using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class EntityReference : MonoBehaviour {
    public EcsPackedEntityWithWorld entityPack;

    public Action OnEntityChanged;
}