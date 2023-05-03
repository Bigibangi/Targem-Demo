using GameLogic.Core.Components;
using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public sealed class InitGameSystem : IEcsInitSystem {
    private readonly EcsWorldInject _defaultWorld = default;
    private readonly EcsSharedInject<SceneSettings> _sceneSettings;

    public void Init(IEcsSystems systems) {
        var entity = _defaultWorld.Value.NewEntity();
        var requestPool = _defaultWorld.Value.GetPool<InitGravitySourceRequest>();
        var initModelRequestPool = _defaultWorld.Value.GetPool<InitModelRequest>();
        requestPool.Add(entity);
        ref var modelRequest = ref initModelRequestPool.Add(entity);
        modelRequest.prefab = _sceneSettings.Value.CenterOfMass;
        var count = _sceneSettings.Value.Count;
        for (int i = 0; i < count; i++) {
            var geometry = _defaultWorld.Value.NewEntity();
            ref var geometryModelRequest = ref initModelRequestPool.Add(geometry);
            geometryModelRequest.prefab = _sceneSettings.Value.GeometryPrefab;
            geometryModelRequest.position = Random.insideUnitSphere * 10f;
        }
    }
}