using GameLogic.InitGeometry.Systems;
using GameLogic.Movement.Systems;
using GameLogic.Physics.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

[DisallowMultipleComponent]
public class EcsStartUp : MonoBehaviour {
    [SerializeField] private SceneSettings _sceneSettings;
    private IEcsSystems _systems;

    #region MonoBehaviour

    private void Start() {
        var world = new EcsWorld();
        _systems = new EcsSystems(world, _sceneSettings);
        AddSystems();
        _systems
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
        .Inject(world)
        .Inject(_sceneSettings)
        .Init();
    }

    private void Update() {
        _systems?.Run();
    }

    private void OnDestroy() {
        if (_systems != null) {
            _systems.GetWorld()?.Destroy();
            _systems.Destroy();
            _systems = null;
        }
    }

    #endregion MonoBehaviour

    private void AddSystems() {
        _systems.
            Add(new InitGameSystem()).
            Add(new CompositeGeometrySystem()).
            Add(new CollisionHandlerSystem()).
            Add(new MovementSystem()).
            Add(new GravitySystem());
    }
}