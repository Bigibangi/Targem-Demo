using GameLogic.Core.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

using static Unity.Mathematics.math;
using float4x4 = Unity.Mathematics.float4x4;

namespace GameLogic.Core {

    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    internal struct UpdateModelJob : IJobFor {

        [ReadOnly]
        public NativeArray<ModelPart> parents;

        public NativeArray<ModelPart> parts;

        [WriteOnly]
        public NativeArray<float4x4> matrices;

        public void Execute(int i) {
            var parent = parents[i/5];
            var part = parts[i];
            matrices[i] = float4x4.TRS(
                part.worldPosition,
                part.worldRotation,
                Vector3.one);
            part.worldRotation = mul(
                parent.worldRotation,
                part.worldRotation);
            part.worldPosition =
                parent.worldPosition +
                mul(parent.worldRotation, parent.scale);
            parts[i] = part;

            matrices[i] = float4x4.TRS(
                part.worldPosition, part.worldRotation, part.scale
            );
        }
    }
}