using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 结点链扩展
    /// </summary>
    public static class NodeChainExtension
    {

        /// <summary>
        /// 从根结点执行结点链
        /// </summary>
        public static void Begin(this IBehaviorNodeChain self)
        {
            GameEntry.BehaviorNodeSystem.ExecuteNode(self as BehaviorNodeBase);
        }

        /// <summary>
        /// 追加事件结点
        /// </summary>
        public static IBehaviorNodeChain Event(this IBehaviorNodeChain self,params GameFrameworkAction[] onExecuteEvents)
        {
            return Event(self,null, null, onExecuteEvents);
        }

        /// <summary>
        /// 追加事件结点
        /// </summary>
        public static IBehaviorNodeChain Event(this IBehaviorNodeChain self,GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params GameFrameworkAction[] onExecuteEvents)
        {
            return self.Append(ReferencePool.Acquire<EventNode>().Fill(onExecuteBegin, onExecuteEnd, onExecuteEvents));
        }

        /// <summary>
        /// 追加延时结点
        /// </summary>
        /// <returns></returns>
        public static IBehaviorNodeChain Delay(this IBehaviorNodeChain self, float delayTime)
        {
            return Delay(self,null, null, delayTime);
        }

        /// <summary>
        /// 追加延时结点
        /// </summary>
        /// <returns></returns>
        public static IBehaviorNodeChain Delay(this IBehaviorNodeChain self, GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, float delayTime)
        {
            return self.Append(ReferencePool.Acquire<DelayNode>().Fill(onExecuteBegin, onExecuteEnd, delayTime)) as IBehaviorNodeChain;
        }

        /// <summary>
        /// 追加序列结点链
        /// </summary>
        public static IBehaviorNodeChain Sequence(this IBehaviorNodeChain self, params BehaviorNodeBase[] nodes)
        {
            return Sequence(self,null, null, nodes);
        }

        /// <summary>
        /// 追加序列结点链
        /// </summary>
        public static IBehaviorNodeChain Sequence(this IBehaviorNodeChain self, GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return self.Append(ReferencePool.Acquire<SequenceNode>().Fill(onExecuteBegin, onExecuteEnd, nodes)) as IBehaviorNodeChain;
        }

        // <summary>
        /// 追加重复结点链
        /// </summary>
        public static IBehaviorNodeChain Repeat(this IBehaviorNodeChain self, int repeatCount, BehaviorNodeBase node)
        {
            return Repeat(self,null, null, repeatCount, node);
        }

        /// <summary>
        /// 追加重复结点链
        /// </summary>
        public static IBehaviorNodeChain Repeat(this IBehaviorNodeChain self, GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, int repeatCount, BehaviorNodeBase node)
        {
            return self.Append(ReferencePool.Acquire<RepeatNode>().Fill(onExecuteBegin, onExecuteEnd, repeatCount, node)) as IBehaviorNodeChain;
        }

        /// <summary>
        /// 追加并行结点链
        /// </summary>
        public static IBehaviorNodeChain Parallel(this IBehaviorNodeChain self, params BehaviorNodeBase[] nodes)
        {
            return Parallel(self,null, null, nodes);
        }

        /// <summary>
        /// 追加并行结点链
        /// </summary>
        public static IBehaviorNodeChain Parallel(this IBehaviorNodeChain self, GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return self.Append(ReferencePool.Acquire<ParallelNode>().Fill(onExecuteBegin, onExecuteEnd, nodes)) as IBehaviorNodeChain;
        }
    }
}

