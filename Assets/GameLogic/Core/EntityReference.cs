using Leopotam.EcsLite;
using UnityEngine;

[DisallowMultipleComponent]
public class EntityReference : MonoBehaviour {
    public EcsPackedEntityWithWorld entityPack;
    public int EntityId;

    public void Awake() {
        entityPack.Unpack(out var world, out EntityId);
    }
}