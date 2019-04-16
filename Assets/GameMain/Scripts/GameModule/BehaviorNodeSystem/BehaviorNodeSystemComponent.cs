using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Trinity
{
    /// <summary>
    /// 行为结点系统组件
    /// </summary>
    public class BehaviorNodeSystemComponent :  GameFrameworkComponent
    {
        /// <summary>
        /// 行为结点链表
        /// </summary>
        private LinkedList<BehaviorNodeBase> m_Nodes = new LinkedList<BehaviorNodeBase>();

        /// <summary>
        /// 是否为序列执行模式
        /// </summary>
        public bool IsSequnceMode;

        private void Update()
        {
            LinkedListNode<BehaviorNodeBase> current = m_Nodes.First;
            while (current != null)
            {
                LinkedListNode<BehaviorNodeBase> temp = current;
                current.Value.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
                if (IsSequnceMode && !current.Value.Finished)
                {
                    //序列执行模式并且当前结点未执行完毕时，结束当前帧的处理
                    break;
                }
                current = temp.Next;
            }
        }

        /// <summary>
        /// 执行结点
        /// </summary>
        public void ExecuteNode(BehaviorNodeBase node)
        {
            Log.Info("添加了行为结点,Type：" + node.GetType());
            m_Nodes.AddLast(node);
        }

        /// <summary>
        /// 移除结点
        /// </summary>
        public void RemoveNode(BehaviorNodeBase node)
        {
            if (m_Nodes.Contains(node))
            {
                Log.Info("删除了行为结点,Type" + node.GetType());
                m_Nodes.Remove(node);
                
            }
        }

        /// <summary>
        /// 开启序列结点链
        /// </summary>
        public SequenceNodeChain Sequence(params BehaviorNodeBase[] nodes)
        {
            return Sequence(null, null,nodes);
        }

        /// <summary>
        /// 开启序列结点链
        /// </summary>
        public SequenceNodeChain Sequence(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return ReferencePool.Acquire<SequenceNodeChain>().Fill(onExecuteBegin,onExecuteEnd,nodes);
        }

        /// <summary>
        /// 开启并行结点链
        /// </summary>
        public BehaviorNodeChainBase Parallel(params BehaviorNodeBase[] nodes)
        {
            return Parallel(null, null, nodes);
        }

        /// <summary>
        /// 开启并行结点链
        /// </summary>
        public BehaviorNodeChainBase Parallel(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            return ReferencePool.Acquire<ParallelNodeChain>().Fill(onExecuteBegin, onExecuteEnd, nodes);
        }

        // <summary>
        /// 开启重复结点链
        /// </summary>
        public BehaviorNodeChainBase Repeat(int repeatCount, params BehaviorNodeBase[] nodes)
        {
            return Repeat(null, null, repeatCount, nodes);
        }

        /// <summary>
        /// 开启重复结点链
        /// </summary>
        public BehaviorNodeChainBase Repeat(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, int repeatCount, params BehaviorNodeBase[] nodes)
        {
            return ReferencePool.Acquire<RepeatNodeChain>().Fill(onExecuteBegin, onExecuteEnd, repeatCount, nodes);
        }

        /// <summary>
        /// 获取事件结点
        /// </summary>
        public EventNode Event(params GameFrameworkAction[] onExecuteEvents)
        {
            return Event(null, null, onExecuteEvents);
        }

        /// <summary>
        /// 获取事件结点
        /// </summary>
        public EventNode Event(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params GameFrameworkAction[] onExecuteEvents)
        {
            return ReferencePool.Acquire<EventNode>().Fill(onExecuteBegin, onExecuteEnd, onExecuteEvents);
        }

        /// <summary>
        /// 获取延时结点
        /// </summary>
        /// <returns></returns>
        public DelayNode Delay(float delayTime)
        {
            return Delay(null, null, delayTime);
        }

        /// <summary>
        /// 获取延时结点
        /// </summary>
        /// <returns></returns>
        public DelayNode Delay(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, float delayTime)
        {
            return ReferencePool.Acquire<DelayNode>().Fill(onExecuteBegin, onExecuteEnd, delayTime);
        }
    }
}

