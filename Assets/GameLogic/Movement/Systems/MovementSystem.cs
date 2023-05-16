using GameLogic.Core.Components;
using GameLogic.Movement.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

namespace GameLogic.Movement.Systems {

    public sealed class MovementSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<Model, Direction, Movable>> _filter;
        private readonly EcsPoolInject<Model> _models;
        private readonly EcsPoolInject<Movable> _movables;
        private readonly EcsPoolInject<Direction> _directions;

        public void Run(IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref var modelComponent = ref _models.Value.Get(entity);
                ref var directionComponent = ref _directions.Value.Get(entity);
                ref var movableComponent = ref _movables.Value.Get(entity) ;

                ref var direction = ref directionComponent.direction;
                ref var worldPosition = ref modelComponent.root.worldPosition;
                ref var velocity = ref movableComponent.velocity;
                ref var maxAcceleration = ref movableComponent.maxAcceleration;
                ref var accelerationForce = ref movableComponent.accelerationForce;
                var desiredVelocity = direction*accelerationForce ;
                var maxSpeedChange = maxAcceleration * Time.deltaTime;
                velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
                velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
                velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
                var displacement = float3(velocity);
                displacement *= Time.deltaTime;
                worldPosition += displacement;
            }
        }
    }
}