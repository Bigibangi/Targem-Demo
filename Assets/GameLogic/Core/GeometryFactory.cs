using GameLogic.Core.Components;
using GameLogic.Gravity.Components;
using GameLogic.Movement.Components;
using GameLogic.Physics.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal class GeometryFactory : AbstractFactory {

        public GeometryFactory(EcsPackedEntityWithWorld packedEntity, GameObject prefab, Vector3 position) : base(packedEntity, prefab, position) {
        }

        public override void InitializeEntityWithPrefab() {
            base.InitializeEntityWithPrefab();
            _packedEntity.Unpack(out var world, out var entity);
            world.GetPool<Attractable>().Add(entity);
            world.GetPool<Forceble>().Add(entity);
            world.GetPool<Movable>().Add(entity);
            world.GetPool<Direction>().Add(entity);
        }
    }
}