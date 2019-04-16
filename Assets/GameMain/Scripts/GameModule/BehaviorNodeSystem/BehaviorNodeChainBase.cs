using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 行为结点链基类
    /// </summary>
    public abstract class BehaviorNodeChainBase : BehaviorNodeBase
    {
        /// <summary>
        /// 结点链所属结点
        /// </summary>
        protected abstract BehaviorNodeBase Node
        {
            get;
        }

        /// <summary>
        /// 追加结点到结点链中
        /// </summary>
        public abstract BehaviorNodeChainBase Append(BehaviorNodeBase node);

        /// <summary>
        /// 从根结点执行结点链
        /// </summary>
        public void Begin()
        {
            GameEntry.BehaviorNodeSystem.ExecuteNode(this);
        }

        protected override void OnExecute(float elapseSeconds, float realElapseSeconds)
        {
            base.OnExecute(elapseSeconds, realElapseSeconds);

            Finished = Node.Execute(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 追加事件结点
        /// </summary>
        public BehaviorNodeChainBase Event(params GameFrameworkAction[] onExecuteEvents)
        {
            return Event(null, null, onExecuteEvents);
        }

        /// <summary>
        /// 追加事件结点
        /// </summary>
        public BehaviorNodeChainBase Event(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd,params GameFrameworkAction[] onExecuteEvents)
        {
            return Append(ReferencePool.Acquire<EventNode>().Fill(onExecuteBegin, onExecuteEnd, onExecuteEvents));
        }

        /// <summary>
        /// 追加延时结点
        /// </summary>
        /// <returns></returns>
        public BehaviorNodeChainBase Delay(float delayTime)
        {
            return Delay(null, null, delayTime);
        }

        /// <summary>
        /// 追加延时结点
        /// </summary>
        /// <returns></returns>
        public BehaviorNodeChainBase Delay(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, float delayTime)
        {
            return Append(ReferencePool.Acquire<DelayNode>().Fill(onExecuteBegin, onExecuteEnd, delayTime));
        }



        /// <summary>
        /// 追加序列结点链
        /// </summary>
        public BehaviorNodeChainBase Sequence(params BehaviorNodeBase[] nodes)
        {
            return Sequence(null, null, nodes);
        }

        /// <summary>
        /// 追加序列结点链
        /// </summary>
        public BehaviorNodeChainBase Sequence(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return Append(ReferencePool.Acquire<SequenceNodeChain>().Fill(onExecuteBegin,onExecuteEnd,nodes));
        }

        // <summary>
        /// 追加重复结点链
        /// </summary>
        public BehaviorNodeChainBase Repeat(int repeatCount, params BehaviorNodeBase[] nodes)
        {
            return Repeat(null, null, repeatCount, nodes);
        }

        /// <summary>
        /// 追加重复结点链
        /// </summary>
        public BehaviorNodeChainBase Repeat(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd,int repeatCount, params BehaviorNodeBase[] nodes)
        {
            return Append(ReferencePool.Acquire<RepeatNodeChain>().Fill(onExecuteBegin, onExecuteEnd,repeatCount,nodes));
        }

        /// <summary>
        /// 追加并行结点链
        /// </summary>
        public BehaviorNodeChainBase Parallel(params BehaviorNodeBase[] nodes)
        {
            return Parallel(null, null, nodes);
        }

        /// <summary>
        /// 追加并行结点链
        /// </summary>
        public BehaviorNodeChainBase Parallel(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return Append(ReferencePool.Acquire<ParallelNodeChain>().Fill(onExecuteBegin, onExecuteEnd, nodes));
        }

    }
}

