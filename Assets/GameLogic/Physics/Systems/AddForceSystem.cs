using GameLogic.Movement.Components;
using GameLogic.Physics.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

using Random = UnityEngine.Random;

namespace GameLogic.Physics.Systems {

    public sealed class AddForceSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<Forceble, Movable, CollisionEventRequest>> _collisioned;
        private readonly EcsPoolInject<Movable> _movables;

        public void Run(IEcsSystems systems) {
            foreach (var entity in _collisioned.Value) {
                ref var movable = ref _movables.Value.Get(entity);
                ref var v = ref movable.velocity;
                var desiredVelocity = Random.insideUnitSphere * Random.Range(20f,40f);
                v = desiredVelocity;
            }
        }
    }
}