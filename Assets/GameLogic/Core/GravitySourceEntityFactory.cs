using GameLogic.Core.Components;
using GameLogic.Gravity.Components;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace GameLogic.Core {

    internal class GravitySourceEntityFactory : EntityFactory {

        public GravitySourceEntityFactory(EcsWorld world) : base(world) {
        }

        public override EcsPackedEntityWithWorld CreateEntity() {
            var packedEntity = base.CreateEntity();
            packedEntity.Unpack(out world, out var entity);
            var gravitySourcePool = world.GetPool<GravitySource>();
            ref var gravitySource = ref gravitySourcePool.Add(entity);
            ref var root = ref world.GetPool<Model>().Get(entity).root;
            root.worldPosition = new float3(0, 0, 0);
            root.worldRotation = quaternion.identity;
            gravitySource.g = 9.81f;
            return packedEntity;
        }
    }
}