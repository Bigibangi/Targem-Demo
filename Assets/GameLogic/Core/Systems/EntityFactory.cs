using Leopotam.EcsLite;

namespace GameLogic.Core.Systems {
    internal abstract class EntityFactory {
        protected EcsWorld world;

        protected EntityFactory(EcsWorld world) {
            this.world = world;
        }

        public virtual int CreateEntity<T>(EcsWorld world, params T[] components) where T : struct {
            var entity = world.NewEntity();
            return entity;
        }
    }
}