using Leopotam.EcsLite;
using UnityEngine;

internal struct CollisionEventRequest {
    public EcsPackedEntityWithWorld senderGameobject;
    public EcsPackedEntityWithWorld targetGameobject;
    public ContactPoint contactPoint;
}