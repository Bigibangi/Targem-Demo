using Unity.Mathematics;

namespace GameLogic.Core.Components {

    internal struct ModelPart {
        public float3 localPosition, worldPosition;
        public quaternion worldRotation;
        public float3 scale;
    }
}