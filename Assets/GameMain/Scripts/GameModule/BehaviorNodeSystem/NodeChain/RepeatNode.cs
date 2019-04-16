using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 重复结点（会重复执行子结点）
    /// </summary>
    public class RepeatNode : BehaviorNodeBase ,IBehaviorNodeChain
    {
        /// <summary>
        /// 要重复执行的结点
        /// </summary>
        private BehaviorNodeBase m_Node;

        /// <summary>
        /// 重复执行次数
        /// </summary>
        public int RepeatCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前已执行次数
        /// </summary>
        private int m_CurrentRepeatCount;

        public IBehaviorNodeChain Append(BehaviorNodeBase node)
        {
            (m_Node as IBehaviorNodeChain).Append(node);
            return this;
        }

        public RepeatNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, int repeatCount,BehaviorNodeBase node)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_Node = node;
            RepeatCount = repeatCount;
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            ReferencePool.Release(m_Node as IReference);
            m_Node = default(BehaviorNodeBase);
            RepeatCount = default(int);
            m_CurrentRepeatCount = default(int);

        }

        protected override void OnReset()
        {
            base.OnReset();
            m_Node?.Reset();
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);
            
            //-1表示无限重复
            if (RepeatCount == -1)
            {
                if (m_Node.Execute(elapseSeconds,realElapseSeconds))
                {
                    m_Node.Reset();
                }

                return;
            }

            ++m_CurrentRepeatCount;
            if (m_Node.Execute(elapseSeconds, realElapseSeconds))
            {
                m_Node.Reset();
            }

            if (m_CurrentRepeatCount >= RepeatCount)
            {
                Finished = true;
            }
        }

      
    }
}

