﻿using GameFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 并行结点
    /// </summary>
    public class ParallelNode : BehaviorNodeBase
    {
        private List<BehaviorNodeBase> m_Nodes = new List<BehaviorNodeBase>();

        public ParallelNode Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            base.Fill(onExecuteBegin, onExecuteEnd);
            m_Nodes.AddRange(nodes);
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            m_Nodes.Clear();
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

        
            foreach (BehaviorNodeBase node in m_Nodes)
            {
                if (!node.Finished)
                {
                    node.Execute(elapseSeconds, realElapseSeconds);
                }
            }

            //只有当所有子结点都执行完毕后，并发执行结点才算执行完毕
            Finished = m_Nodes.All(node => node.Finished);
        }

        public ParallelNode Append(BehaviorNodeBase node)
        {
            m_Nodes.Add(node);
            return this;
        }

    }

}
