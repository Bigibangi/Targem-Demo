using GameLogic.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public sealed class InitGameSystem : IEcsInitSystem {
    private readonly EcsWorldInject _defaultWorld = default;
    private readonly EcsSharedInject<SceneSettings> _sceneSettings;

    public void Init(IEcsSystems systems) {
        var entity = _defaultWorld.Value.NewEntity();
        var packedEntity = _defaultWorld.Value.PackEntityWithWorld(entity);
        var gsFactory = new GravitySourceFactory(packedEntity,_sceneSettings.Value.CenterOfMass, Vector3.zero);
        gsFactory.InitializeEntityWithPrefab();
        var count = _sceneSettings.Value.Count;
        for (int i = 0; i < count; i++) {
            var geometry = _defaultWorld.Value.NewEntity();
            packedEntity = _defaultWorld.Value.PackEntityWithWorld(geometry);
            var gFactory = new GeometryFactory(
                packedEntity,
                _sceneSettings.Value.GeometryPrefab,
                Random.insideUnitSphere * 10f
                );
            gFactory.InitializeEntityWithPrefab();
        }
    }
}