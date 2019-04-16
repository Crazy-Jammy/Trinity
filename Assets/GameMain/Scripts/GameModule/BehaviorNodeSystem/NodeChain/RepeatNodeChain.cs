using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 重复执行结点链
    /// </summary>
    public class RepeatNodeChain : BehaviorNodeChainBase
    {
        /// <summary>
        /// 重复执行结点
        /// </summary>
        private RepeatNode m_RepeatNode;

        /// <summary>
        /// 序列执行结点
        /// </summary>
        private SequenceNode m_SequenceNode;

        protected override BehaviorNodeBase Node
        {
            get
            {
               return m_RepeatNode;
            }
        }

        public override BehaviorNodeChainBase Append(BehaviorNodeBase node)
        {
            m_SequenceNode.Append(node);
            return this;
        }

        public RepeatNodeChain Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd,int repeatCount,params BehaviorNodeBase[] nodes)
        {
            base.Fill(onExecuteBegin,onExecuteEnd);
            m_SequenceNode = ReferencePool.Acquire<SequenceNode>().Fill(null, null, nodes);
            m_RepeatNode = ReferencePool.Acquire<RepeatNode>().Fill(null, null,m_SequenceNode, repeatCount);
            return this;
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}

