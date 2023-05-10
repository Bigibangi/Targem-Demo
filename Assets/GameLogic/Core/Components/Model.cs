using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace GameLogic.Core.Components {

    internal struct Model {
        public Transform modelTransform;
        public NativeArray<ModelPart> parts;
        public NativeArray<float4x4> matrices;
    }
}