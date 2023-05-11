using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace GameLogic.Core {

    internal abstract class AbstractFactory {
        protected EcsPackedEntityWithWorld _packedEntity;
        protected GameObject _prefab;
        protected Vector3 _position;
        protected GameObject _instance;

        protected AbstractFactory(
            EcsPackedEntityWithWorld packedEntity,
            GameObject prefab,
            Vector3 position) {
            _packedEntity = packedEntity;
            _prefab = prefab;
            _position = position;
        }

        protected AbstractFactory() {
        }

        public virtual void InitializeEntityWithPrefab() {
            _packedEntity.Unpack(out var defaultWorld, out var entity);
            var modelPool = defaultWorld.GetPool<Model>();
            ref var model = ref modelPool.Add(entity);
            _instance = Object.Instantiate(_prefab, _position, Quaternion.identity);
            var ecsRef = _instance.AddComponent<EntityReference>();
            ecsRef.entityPack = _packedEntity;
            var root = new ModelPart{
                worldPosition = _position,
                worldRotation = quaternion.identity
            };
            model.root = root;
        }
    }
}