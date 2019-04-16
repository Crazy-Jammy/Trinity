using GameFramework;
using System.Threading.Tasks;

namespace Trinity
{
    /// <summary>
    /// Buff基类
    /// </summary>
    public abstract class BuffBase :IReference
    {
        

        /// <summary>
        /// Buff持有者
        /// </summary>
        public Entity Owner
        {
            get;
            private set;
        }

        /// <summary>
        /// Buff持续时间
        /// </summary>
        public float? Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// Buff重复生效间隔
        /// </summary>
        public float RepeatTakeEffectInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// Buff附加时
        /// </summary>
        public async void OnAttach()
        {
            SubscribeTimePointEvent();
            TakeEffect();

            if (Duration.HasValue)
            {
                await Task.Delay((int)(Duration * 1000));
                //解除Buff
                GameEntry.SkillSystem.DetachBuff(this);
            }
        }

        

        /// <summary>
        /// Buff解除时
        /// </summary>
        public void OnDetach()
        {
            UnsubscribeTimePointEvent();
            UntakeEffect();
        }

        /// <summary>
        /// Buff生效
        /// </summary>
        protected abstract void TakeEffect();

        /// <summary>
        /// Buff失效时
        /// </summary>
        protected abstract void UntakeEffect();

        /// <summary>
        /// Buff轮询时
        /// </summary>
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 订阅时点事件
        /// </summary>
        protected virtual void SubscribeTimePointEvent()
        {

        }

        /// <summary>
        /// 取消订阅时点事件
        /// </summary>
        protected virtual void UnsubscribeTimePointEvent()
        {

        }



        protected BuffBase Fill(Entity owner, float duration,float repeatTakeEffectInterval)
        {
            Owner = owner;
            Duration = duration;
            RepeatTakeEffectInterval = RepeatTakeEffectInterval;
            return this;    
        }


        public virtual void Clear()
        {
            Owner = default(Entity);
            Duration = default(float);
            RepeatTakeEffectInterval = default(float);
        }
    }
}

