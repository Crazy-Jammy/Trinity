using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 重复执行结点
    /// </summary>
    public class RepeatNode : BehaviorNodeBase
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

        /// <summary>
        /// 是否已重复执行完毕
        /// </summary>
        public bool Completed
        {
            get;
            private set;
        }

        public RepeatNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd,BehaviorNodeBase node,int repeatCount)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_Node = node;
            RepeatCount = repeatCount;
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            m_Node = default(BehaviorNodeBase);
            RepeatCount = default(int);
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

            if (m_Node.Execute(elapseSeconds, realElapseSeconds))
            {
                m_Node.Reset();
                ++m_CurrentRepeatCount;
            }

            if (m_CurrentRepeatCount == RepeatCount)
            {
                Finished = true;
                Completed = true;
            }
        }
    }
}

