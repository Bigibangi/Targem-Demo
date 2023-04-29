using GameLogic.Gravity.Components;
using GameLogic.InitGeometry.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.InitGeometry.Systems {

    public sealed class InitializeGeometrySystem : IEcsInitSystem, IEcsRunSystem {
        private SceneSettings _sceneSettings;
        private EcsWorld _world;

        public void Init(IEcsSystems systems) {
            _world ??= systems.GetWorld();
            _sceneSettings ??= systems.GetShared<SceneSettings>();
            var count = _sceneSettings.Count;
            var modelPool = _world.GetPool<Model>();
            var attractPool = _world.GetPool<Attractable>();
            for (int i = 0; i < count; i++) {
                var entity = _world.NewEntity();
                ref var modelComponent = ref modelPool.Add(entity);
                ref var attractbleComponent = ref attractPool.Add(entity);
                ref var transform = ref modelComponent.modelTransform;
                var cube = Object.Instantiate(_sceneSettings.GeometryPrefab);
                transform = cube.transform;
                transform.localPosition = Random.insideUnitSphere * 10f;
            }
        }

        public void Run(IEcsSystems systems) {
        }
    }
}