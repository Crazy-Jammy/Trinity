using GameFramework;

namespace Trinity
{
    
    /// <summary>
    /// 等待条件结点
    /// </summary>
    public class AwaitConditionNode : BehaviorNodeBase
    {
        /// <summary>
        /// 执行完毕的条件
        /// </summary>
        private GameFrameworkFunc<bool> m_Condition;

        public AwaitConditionNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd,GameFrameworkFunc<bool> condition)
        {
            base.Fill(onExecuteBegin,onExecuteEnd);
            m_Condition = condition;
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            m_Condition = null;
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            if (m_Condition != null)
            {
                Finished = m_Condition();
            } 
        }
    }
}

