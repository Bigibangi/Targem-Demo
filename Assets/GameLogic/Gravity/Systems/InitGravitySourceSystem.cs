using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Gravity.Systems {

    public class InitGravitySourceSystem : IEcsInitSystem, IEcsRunSystem {
        private EcsWorld _world;
        private EcsFilter _gravityInitFilter;
        private EcsPool<InitGravitySourceRequest> _requestsPool;
        private EcsPool<GravitySource> _gravitySourcePool;
        private SceneSettings _sceneSettings;

        public void Init(IEcsSystems systems) {
            _world ??= systems.GetWorld();
            _gravityInitFilter ??= _world.Filter<InitGravitySourceRequest>().End();
            _requestsPool ??= _world.GetPool<InitGravitySourceRequest>();
            _gravitySourcePool ??= _world.GetPool<GravitySource>();
            _sceneSettings ??= systems.GetShared<SceneSettings>();
        }

        public void Run(IEcsSystems systems) {
            foreach (var entity in _gravityInitFilter) {
                ref var gravitySource = ref _gravitySourcePool.Add(entity);
                gravitySource.g = 9.81f;
                gravitySource.position = Vector3.zero;
                var centerOfMass = Object.Instantiate(_sceneSettings.CenterOfMass);
                centerOfMass.transform.position = gravitySource.position;
                _requestsPool.Del(entity);
            }
        }
    }
}