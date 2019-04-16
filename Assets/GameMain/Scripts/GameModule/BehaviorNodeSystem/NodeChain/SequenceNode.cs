using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 序列结点
    /// </summary>
    public class SequenceNode : BehaviorNodeBase
    {

        /// <summary>
        /// 已执行完毕的子结点
        /// </summary>
        private List<BehaviorNodeBase> m_Nodes = new List<BehaviorNodeBase>();

        /// <summary>
        /// 需要执行的子结点
        /// </summary>
        private List<BehaviorNodeBase> m_NeedExcuteChilds = new List<BehaviorNodeBase>();



        /// <summary>
        /// 追加结点到序列结点中
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public SequenceNode Append(BehaviorNodeBase node)
        {
            m_NeedExcuteChilds.Add(node);
            return this;
        }

        public SequenceNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_NeedExcuteChilds.AddRange(nodes);
            return this;
        }

        protected override void OnReset()
        {
            base.OnReset();

            m_NeedExcuteChilds.Clear();
            foreach (BehaviorNodeBase node in m_Nodes)
            {
                node.Reset();
                m_NeedExcuteChilds.Add(node);
            }        
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            if (m_NeedExcuteChilds.Count > 0)
            {
                //执行第一个子结点
                if (m_NeedExcuteChilds[0].Execute(elapseSeconds, realElapseSeconds))
                {
                    m_Nodes.Add(m_NeedExcuteChilds[0]);
                    m_NeedExcuteChilds.RemoveAt(0);
                    
                }
            }

            //序列结点是否执行完毕，是根据其子结点是否全部执行完毕来判断的
            Finished = m_NeedExcuteChilds.Count == 0;
        }

      


    }
}

