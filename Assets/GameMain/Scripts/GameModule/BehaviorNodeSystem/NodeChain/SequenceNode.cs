using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 序列结点(只有将前面的子结点执行完毕才会执行后面的）
    /// </summary>
    public class SequenceNode : BehaviorNodeBase , IBehaviorNodeChain
    {

        /// <summary>
        /// 所有子结点
        /// </summary>
        private List<BehaviorNodeBase> m_Nodes = new List<BehaviorNodeBase>();

        /// <summary>
        /// 需要执行的子结点
        /// </summary>
        private Queue<BehaviorNodeBase> m_NeedExcuteChilds = new Queue<BehaviorNodeBase>();

        public IBehaviorNodeChain Append(BehaviorNodeBase node)
        {
            m_Nodes.Add(node);
            m_NeedExcuteChilds.Enqueue(node);
            return this;
        }

        public SequenceNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_Nodes.AddRange(nodes);

            m_NeedExcuteChilds.Clear();
            foreach (BehaviorNodeBase node in nodes)
            {
                m_NeedExcuteChilds.Enqueue(node);
            }
            

            return this;
        }

        public override void Clear()
        {

            base.Clear();
            foreach (BehaviorNodeBase node in m_Nodes)
            {
                node.Reset();
                ReferencePool.Release(node as IReference);
            }
            m_Nodes.Clear();
        }

        protected override void OnReset()
        {
            base.OnReset();

            m_NeedExcuteChilds.Clear();
            foreach (BehaviorNodeBase node in m_Nodes)
            {
                node.Reset();
                m_NeedExcuteChilds.Enqueue(node);
            }        
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            if (m_NeedExcuteChilds.Count > 0)
            {
                //执行第一个子结点
                if (m_NeedExcuteChilds.Peek().Execute(elapseSeconds, realElapseSeconds))
                {
                    m_NeedExcuteChilds.Dequeue();
                }
            }

            //序列结点是否执行完毕，是根据其子结点是否全部执行完毕来判断的
            Finished = m_NeedExcuteChilds.Count == 0;
        }

       
    }
}

