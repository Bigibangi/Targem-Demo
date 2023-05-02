using GameLogic.Gravity.Systems;
using GameLogic.InitGeometry.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

[DisallowMultipleComponent]
public class EcsStartUp : MonoBehaviour {
    [SerializeField] private SceneSettings _sceneSettings;
    private EcsWorld _world;
    private IEcsSystems _systems;

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
        .Init();
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
        _systems.
            Add(new InitGameSystem()).
            Add(new CompositeGeometrySystem()).
            Add(new InitGravitySourceSystem()).
            Add(new InitializeGeometrySystem()).
            Add(new GravitySystem());
    }
}