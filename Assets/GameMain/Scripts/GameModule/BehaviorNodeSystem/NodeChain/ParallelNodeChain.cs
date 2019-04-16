using GameFramework;

namespace Trinity
{
    /// <summary>
    /// 并行结点链
    /// </summary>
    public class ParallelNodeChain : BehaviorNodeChainBase
    {
        private ParallelNode m_Node;

        protected override BehaviorNodeBase Node
        {
            get
            {
                return m_Node;
            }
        }

        public override BehaviorNodeChainBase Append(BehaviorNodeBase node)
        {
            m_Node.Append(node);
            return this;
        }

        public ParallelNodeChain Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            m_Node = ReferencePool.Acquire<ParallelNode>().Fill(onExecuteBegin,onExecuteEnd,nodes);
            return this;
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}

