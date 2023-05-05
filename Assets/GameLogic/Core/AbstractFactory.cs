using GameLogic.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal abstract class AbstractFactory {
        protected EcsPackedEntityWithWorld _packedEntity;
        protected GameObject _prefab;
        protected Vector3 _position;

        protected AbstractFactory(
            EcsPackedEntityWithWorld packedEntity,
            GameObject prefab,
            Vector3 position) {
            _packedEntity = packedEntity;
            _prefab = prefab;
            _position = position;
        }

        public virtual void InitializeEntityWithPrefab() {
            _packedEntity.Unpack(out var defaultWorld, out var entity);
            var modelPool = defaultWorld.GetPool<Model>();
            ref var model = ref modelPool.Add(entity);
            var obj = Object.Instantiate(_prefab,_position,Quaternion.identity);
            var ecsRef = obj.AddComponent<EntityReference>();
            ecsRef.entityPack = _packedEntity;
            model.modelTransform = obj.transform;
        }
    }
}