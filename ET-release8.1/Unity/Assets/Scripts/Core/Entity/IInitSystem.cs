using System;

namespace ET
{
    public interface IInit
    {
 
    }
    public interface IInitSystem : ISystemType
    {
        void Run(Entity o);
    }

    [EntitySystem]
    public abstract class InitSystem<T> : SystemObject, IInitSystem where T : Entity, IInit
    {
        void IInitSystem.Run(Entity o)
        {
            this.Init((T)o);
        }

        Type ISystemType.SystemType()
        {
            return typeof(IInitSystem);
        }

        int ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        Type ISystemType.Type()
        {
            return typeof(T);
        }

        protected abstract void Init(T self);
    }
}
