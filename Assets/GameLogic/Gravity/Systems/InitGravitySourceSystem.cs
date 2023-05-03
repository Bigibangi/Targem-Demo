using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace GameLogic.Gravity.Systems {

    public sealed class InitGravitySourceSystem : IEcsRunSystem {
        private EcsFilterInject<Inc<InitGravitySourceRequest>> _gravityInitFilter;
        private EcsPoolInject<InitGravitySourceRequest> _requestsPool;
        private EcsPoolInject<GravitySource> _gravitySourcePool;

        public void Run(IEcsSystems systems) {
            foreach (var entity in _gravityInitFilter.Value) {
                ref var gravitySource = ref _gravitySourcePool.Value.Add(entity);
                gravitySource.g = 9.81f;
                gravitySource.position = Vector3.zero;
                _requestsPool.Value.Del(entity);
            }
        }
    }
}