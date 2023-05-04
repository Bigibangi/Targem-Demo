using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public sealed class CollisionHandlerSystem : IEcsRunSystem {
    private readonly EcsFilterInject<Inc<CollisionEventRequest>> _collisionedObject;
    private readonly EcsPoolInject<CollisionEventRequest> _collisionPool;

    public void Run(IEcsSystems systems) {
        foreach (var collisionRequest in _collisionedObject.Value) {
            ref var collision = ref _collisionedObject.Pools.Inc1.Get(collisionRequest);
            var pack = collision.senderGameobject;
            pack.Unpack(out var world, out var entity);
            _collisionPool.Value.Del(collisionRequest);
        }
    }
}