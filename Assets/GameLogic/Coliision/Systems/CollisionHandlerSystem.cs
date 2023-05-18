using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class CollisionHandlerSystem : IEcsRunSystem {
    private readonly EcsFilterInject<Inc<CollisionEventRequest>> _collisionedObject;
    private readonly EcsPoolInject<CollisionEventRequest> _collisionPool;
    private readonly EcsCustomInject<UIScreen> _uiScreen;

    public void Run(IEcsSystems systems) {
        foreach (var collisionRequest in _collisionedObject.Value) {
            ref var collision = ref _collisionedObject.Pools.Inc1.Get(collisionRequest);
            collision.senderEntityPack.Unpack(out var world, out var senderEntity);
            collision.targetEntityPack.Unpack(out world, out var targetEntity);
            _uiScreen.Value.OnCollisionChanged?.Invoke(1);
            _collisionPool.Value.Del(collisionRequest);
        }
    }
}