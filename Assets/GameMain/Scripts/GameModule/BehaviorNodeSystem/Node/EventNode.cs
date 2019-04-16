using GameFramework;
using System.Collections.Generic;

namespace Trinity
{
    /// <summary>
    /// 事件行为结点
    /// </summary>
    public class EventNode : BehaviorNodeBase
    {
        /// <summary>
        /// 结点执行时回调
        /// </summary>
        private List<GameFrameworkAction> m_OnExecuteEvents = new List<GameFrameworkAction>();

        public EventNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params GameFrameworkAction[] onExecuteEvents)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_OnExecuteEvents.AddRange(onExecuteEvents);
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            m_OnExecuteEvents.Clear();
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            if (m_OnExecuteEvents.Count > 0)
            {
                foreach (GameFrameworkAction action in m_OnExecuteEvents)
                {
                    action?.Invoke();
                }
            }

            Finished = true;
        }

      
    }


}

