using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace GameLogic.Core.Components {

    internal struct Model {
        public ModelPart root;
        public NativeArray<ModelPart>[] parts;
        public NativeArray<float4x4>[] matrices;
    }
}