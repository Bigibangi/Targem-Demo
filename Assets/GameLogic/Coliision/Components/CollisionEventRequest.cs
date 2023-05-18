using Leopotam.EcsLite;

internal struct CollisionEventRequest {
    public EcsPackedEntityWithWorld senderEntityPack;
    public EcsPackedEntityWithWorld targetEntityPack;
}