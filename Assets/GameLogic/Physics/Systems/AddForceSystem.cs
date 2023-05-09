using GameLogic.Movement.Components;
using GameLogic.Physics.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Physics.Systems {

    public class AddForceSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<Forceble, Movable, CollisionEventRequest>> _collisioned;
        private readonly EcsPoolInject<Movable> _movables;
        private readonly EcsPoolInject<Forceble> _forcebles;

        public void Run(IEcsSystems systems) {
            foreach (var entity in _collisioned.Value) {
                var collisionPool = _collisioned.Pools.Inc3;
                var collision = _collisioned.Pools.Inc3.Get(entity);
                var pack = collision.targetGameobject;
                pack.Unpack(out var world, out var targetEntity);
                if (!collisionPool.Has(targetEntity)) continue;
                ref var forceble = ref _forcebles.Value.Get(entity);
                ref var movable = ref _movables.Value.Get(entity);
                ref var v = ref movable.velocity;
                var desiredVelocity = Random.insideUnitSphere * Random.Range(20f,40f);
                v = desiredVelocity;
            }
        }
    }
}