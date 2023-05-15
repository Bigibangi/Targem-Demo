using GameLogic.Core.Components;
using Leopotam.EcsLite;

namespace GameLogic.Core {

    internal abstract class EntityFactory {
        protected EcsWorld world;

        protected EntityFactory(EcsWorld world) {
            this.world = world;
        }

        public virtual EcsPackedEntityWithWorld CreateEntity() {
            var entity = world.NewEntity();
            var model = world.GetPool<Model>().Add(entity);
            model.root = new ModelPart();
            return world.PackEntityWithWorld(entity);
        }
    }
}