using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using log4net.Util;
using UnityEngine;

public sealed class GravitySystem : IEcsRunSystem {
    private readonly EcsFilterInject<Inc<Model, Attractable>> _attractables = default;
    private readonly EcsFilterInject<Inc<GravitySource>> _gravitySources = default;
    private readonly EcsWorldInject _defaultWorld = default;

    public void Run(IEcsSystems systems) {
        var a = _defaultWorld.Value;
        foreach (var gravityEntity in _gravitySources.Value) {
            ref var gravity = ref _gravitySources.Pools.Inc1.Get(gravityEntity);
            ref var g = ref gravity.g;
            ref var position = ref gravity.position;
            foreach (var entity in _attractables.Value) {
                ref var model = ref _attractables.Pools.Inc1.Get(entity);
                ref var attractable = ref _attractables.Pools.Inc2.Get(entity);
                ref var velocity = ref attractable.velocity;
                ref var speed = ref attractable.speed;
                var modelPosition = model.modelTransform.localPosition;
                var direction = position - modelPosition;
                var maxSpeedChange = g * Time.deltaTime;
                var desiredVelocity = direction * maxSpeedChange;
                velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
                velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
                velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
                model.modelTransform.localPosition += velocity;
            }
        }
    }
}