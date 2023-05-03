using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal class GravitySourceFactory : AbstractFactory {

        public GravitySourceFactory(EcsPackedEntityWithWorld packedEntity, GameObject prefab, Vector3 position) : base(packedEntity, prefab, position) {
        }

        public override void InitializeEntityWithPrefab() {
            base.InitializeEntityWithPrefab();
            _packedEntity.Unpack(out var world, out var entity);
            var gravitySourcePool = world.GetPool<GravitySource>();
            ref var gravitySource = ref gravitySourcePool.Add(entity);
            gravitySource.g = 9.81f;
            gravitySource.position = Vector3.zero;
        }
    }
}