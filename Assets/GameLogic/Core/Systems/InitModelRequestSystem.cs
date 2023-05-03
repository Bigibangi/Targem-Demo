using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.GameLogic.Core.Systems {

    public sealed class InitModelRequestSystem : IEcsRunSystem {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsFilterInject<Inc<InitModelRequest>> _initModelFilter = default;
        private readonly EcsPoolInject<InitModelRequest> _initModelPool = default;
        private readonly EcsPoolInject<Model> _modelPool = default;

        public void Run(IEcsSystems systems) {
            foreach (var initModel in _initModelFilter.Value) {
                ref var model = ref _modelPool.Value.Add(initModel);
                var initModelRequest = _initModelPool.Value.Get(initModel);
                var obj = Object.Instantiate(initModelRequest.prefab,initModelRequest.position,Quaternion.identity);
                var ecsRef = obj.AddComponent<EntityReference>();
                ecsRef.entityPack = _defaultWorld.Value.PackEntityWithWorld(initModel);
                model.modelTransform = obj.transform;
                _initModelPool.Value.Del(initModel);
            }
        }
    }
}