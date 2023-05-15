using GameLogic.Core.Components;
using GameLogic.Gravity.Components;
using GameLogic.Movement.Components;
using GameLogic.Physics.Components;
using Leopotam.EcsLite;
using Unity.Collections;
using Unity.Mathematics;

using static Unity.Mathematics.math;
using quaternion = Unity.Mathematics.quaternion;

namespace GameLogic.Core {

    internal class GeometryEntityFactory : EntityFactory {

        private static float3[] directions = {
        up(), right(), left(), forward(), back(), down()
    };

        private static quaternion[] rotations = {
        quaternion.identity,
        quaternion.RotateZ(-0.5f * PI), quaternion.RotateZ(0.5f * PI),
        quaternion.RotateX(0.5f * PI), quaternion.RotateX(-0.5f * PI),
        quaternion.identity
    };

        public GeometryEntityFactory(EcsWorld world) : base(world) {
        }

        public override EcsPackedEntityWithWorld CreateEntity() {
            var packedEntity = base.CreateEntity();
            packedEntity.Unpack(out world, out var entity);
            world.GetPool<Attractable>().Add(entity);
            world.GetPool<Forceble>().Add(entity);
            world.GetPool<Movable>().Add(entity);
            world.GetPool<Direction>().Add(entity);
            ref var model = ref world.GetPool<Model>().Get(entity);
            model.depth = 2;
            model.parts = new NativeArray<ModelPart>[model.depth];
            model.matrices = new NativeArray<float4x4>[model.depth];
            for (int i = 0, length = 1; i < model.parts.Length; i++, length *= 5) {
                model.parts[i] = new NativeArray<ModelPart>(length, Allocator.Persistent);
                model.matrices[i] = new NativeArray<float4x4>(length, Allocator.Persistent);
            }
            for (int li = 1; li < model.parts.Length; li++) {
                var levelParts = model.parts[li];
                for (int fpi = 0; fpi < levelParts.Length; fpi += 5) {
                    for (int ci = 0; ci < 5; ci++) {
                        levelParts[fpi + ci] = CreatePart(ci);
                    }
                }
            }
            return packedEntity;
        }

        private ModelPart CreatePart(int ci) =>
            new ModelPart {
                worldRotation = rotations[ci]
            };
    }
}