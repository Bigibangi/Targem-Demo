using GameLogic.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class InitGameSystem : IEcsInitSystem {
    private readonly EcsWorldInject _defaultWorld = default;
    private readonly EcsSharedInject<SceneSettings> _sceneSettings;

    public void Init(IEcsSystems systems) {
        var gsFactory = new GravitySourceViewFactory(
            new GravitySourceEntityFactory(_defaultWorld.Value).CreateEntity(),
            _sceneSettings.Value.CenterOfMass);
        gsFactory.InstatiateView();
        var count = _sceneSettings.Value.Count;
        for (int i = 0; i < count; i++) {
            var gFactory = new GeometryViewFactory(
                new GeometryEntityFactory(_defaultWorld.Value).CreateEntity(),
                _sceneSettings.Value.GeometryPrefab);
            gFactory.InitializeEntityWithProceduralDraw();
        }
    }
}