using System;
using Leopotam.EcsLite;
using UnityEngine;

public class EcsStartUp : MonoBehaviour {
    private EcsWorld _world;
    private IEcsSystems _systems;

    #region MonoBehaviour

    private void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _systems
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        AddInjections();
        AddOneFrames();
        AddSystems();
    }

    private void Update() {
        _systems?.Run();
    }

    private void OnDestroy() {
        if (_systems != null) {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }

    #endregion MonoBehaviour

    private void AddSystems() {
        throw new NotImplementedException();
    }

    private void AddOneFrames() {
        throw new NotImplementedException();
    }

    private void AddInjections() {
        throw new NotImplementedException();
    }
}