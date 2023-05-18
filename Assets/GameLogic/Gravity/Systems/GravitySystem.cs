using GameLogic.Core.Components;
using GameLogic.Gravity.Components;
using GameLogic.Movement.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

public sealed class GravitySystem : IEcsRunSystem {
    private readonly EcsFilterInject<Inc<Model, Attractable, Movable>> _attractablesFilter = default;
    private readonly EcsFilterInject<Inc<GravitySource, Model>> _gravitySources = default;
    private readonly EcsPoolInject<Direction> _directions;

    public void Run(IEcsSystems systems) {
        var gravitySource = _gravitySources.Value.GetRawEntities().First();
        ref var gravity = ref _gravitySources.Pools.Inc1.Get(gravitySource);
        ref var gravityModel = ref _gravitySources.Pools.Inc2.Get(gravitySource);
        ref var g = ref gravity.g;
        foreach (var entity in _attractablesFilter.Value) {
            ref var model = ref _attractablesFilter.Pools.Inc1.Get(entity);
            ref var movable = ref _attractablesFilter.Pools.Inc3.Get(entity);
            ref var directionComponent = ref _directions.Value.Get(entity);
            ref var direction = ref directionComponent.direction;
            ref var velocity = ref movable.velocity;
            var modelPosition = model.root.worldPosition;
            direction = gravityModel.root.worldPosition - modelPosition;
            movable.accelerationForce = g / direction.magnitude;
            var maxSpeedChange = g * Time.deltaTime;
            var desiredVelocity = direction * maxSpeedChange;
            velocity += desiredVelocity;
        }
    }
}