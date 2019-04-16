using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 延时结点
    /// </summary>
    public class DelayNode : BehaviorNodeBase
    {
        private float m_DelayTime;

        private float m_Timer;

        public DelayNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, float delayTime)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_DelayTime = delayTime;
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            m_DelayTime = default(float);
            m_Timer = 0;
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            m_Timer += elapseSeconds;
            Finished = m_Timer >= m_DelayTime;
        }

       
    }
}

