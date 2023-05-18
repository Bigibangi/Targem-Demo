using Client;
using GameLogic.Core.Components;
using GameLogic.Core.Systems;
using GameLogic.Movement.Systems;
using GameLogic.Physics.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UI;
using UnityEngine;

[DisallowMultipleComponent]
public class EcsStartUp : MonoBehaviour {
    [SerializeField] private SceneSettings _sceneSettings;
    [SerializeField] private EcsUguiEmitter _emitter;
    [SerializeField] private UIScreen _screen;
    private IEcsSystems _systems;
    private EcsWorld _world;

    #region MonoBehaviour

    private void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world, _sceneSettings);
        AddSystems();
        _systems
            .AddWorld(new EcsWorld(), Idents.Worlds.Events)
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
        .Inject(_world)
        .Inject(_screen)
        .Inject(_sceneSettings)
        .InjectUgui(_emitter)
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
            Add(new AddForceSystem()).
            Add(new CollisionHandlerSystem()).
            Add(new MovementSystem()).
            Add(new GravitySystem()).
            Add(new UIButtonsHandlerSystem()).
            Add(new UpdateModelJobSystem());
    }
}