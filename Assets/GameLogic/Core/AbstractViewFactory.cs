using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal abstract class AbstractViewFactory {
        protected EcsPackedEntityWithWorld _packedEntity;
        protected GameObject _prefab;

        protected AbstractViewFactory(
            EcsPackedEntityWithWorld packedEntity,
            GameObject prefab) {
            _packedEntity = packedEntity;
            _prefab = prefab;
        }

        public virtual void InstatiateView() {
            var obj = Object.Instantiate(_prefab);
            if (obj.TryGetComponent<EntityReference>(out var ecsRef)) {
                ecsRef.entityPack = _packedEntity;
                ecsRef.OnEntityChanged?.Invoke();
            }
        }
    }
}