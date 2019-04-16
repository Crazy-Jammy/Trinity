
using GameFramework;

namespace Trinity
{

    /// <summary>
    /// 行为结点基类
    /// </summary>
    public abstract class BehaviorNodeBase : IReference
    {
       
        /// <summary>
        /// 结点执行开始回调
        /// </summary>
        private GameFrameworkAction m_OnExecuteBegin;

        /// <summary>
        /// 结点执行结束回调
        /// </summary>
        private GameFrameworkAction m_OnExecuteEnd;

        /// <summary>
        /// 是否调用过结点执行开始回调
        /// </summary>
        protected bool m_OnExecuteBegined = false;

        /// <summary>
        /// 结点是否已执行完毕
        /// </summary>
        public bool Finished
        {
            get;
            protected set;
        }

        /// <summary>
        /// 轮询结点
        /// </summary>
        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (Finished)
            {
                Reset();
            }
            else
            {
                Execute(elapseSeconds, realElapseSeconds);
            } 
        }

        /// <summary>
        /// 执行结点
        /// </summary>
        public bool Execute(float elapseSeconds, float realElapseSeconds)
        {
            //调用结点开始执行时的回调
            if (!m_OnExecuteBegined)
            {
                m_OnExecuteBegined = true;
                OnBegin();
                m_OnExecuteBegin?.Invoke();
            }

            //执行结点
            if (!Finished)
            {
                OnExecute(elapseSeconds, realElapseSeconds);
            }

            //如果执行完毕，调用完毕时的回调
            if (Finished)
            {
                OnEnd();
                m_OnExecuteEnd?.Invoke();
            }

            return Finished;

        }

        /// <summary>
        /// 结点重置
        /// </summary>
        public void Reset()
        {
            Finished = false;
            m_OnExecuteBegined = false;
            OnReset();
        }

        /// <summary>
        /// 结点强制结束
        /// </summary>
        public void Break()
        {
            Finished = true;
        }

        /// <summary>
        /// 结点重置时
        /// </summary>
        protected virtual void OnReset()
        {
        }

        /// <summary>
        /// 结点执行开始时
        /// </summary>
        protected virtual void OnBegin()
        {
        }

        /// <summary>
        /// 结点执行时
        /// </summary>
        protected virtual void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 结点执行结束时
        /// </summary>
        protected virtual void OnEnd()
        {
        }

        public BehaviorNodeBase Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd)
        {
            m_OnExecuteBegin = onExecuteBegin;
            m_OnExecuteEnd = onExecuteEnd;
            return this;
        }


        public virtual void Clear()
        {
            Reset();
            m_OnExecuteBegin = default(GameFrameworkAction);
            m_OnExecuteEnd = default(GameFrameworkAction);
            
        }
    }
}

