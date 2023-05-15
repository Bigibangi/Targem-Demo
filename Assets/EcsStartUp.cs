using GameLogic.Core.Components;
using GameLogic.Core.Systems;
using GameLogic.Movement.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

[DisallowMultipleComponent]
public class EcsStartUp : MonoBehaviour {
    [SerializeField] private SceneSettings _sceneSettings;
    private IEcsSystems _systems;
    private EcsWorld _world;

    #region MonoBehaviour

    private void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world, _sceneSettings);
        AddSystems();
        _systems
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
        .Inject(_world)
        .Inject(_sceneSettings)
        .Init();
    }

    private void Update() {
        _systems?.Run();
    }

    private void OnDestroy() {
        if (_systems != null) {
            _systems.Destroy();
            _systems = null;
        }
        if (_world != null) {
            var modelPool = _world.GetPool<Model>();
            foreach (var model in modelPool.GetRawDenseItems()) {
                model.Dispose();
            }
        }
    }

    #endregion MonoBehaviour

    private void AddSystems() {
        _systems.
            Add(new InitGameSystem()).
            Add(new CollisionHandlerSystem()).
            Add(new MovementSystem()).
            Add(new GravitySystem()).
            Add(new UpdateModelJobSystem());
    }
}