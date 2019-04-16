using GameFramework;    

namespace Trinity
{
    /// <summary>
    /// 序列结点链
    /// </summary>
    public class SequenceNodeChain : BehaviorNodeChainBase
    {
        private SequenceNode m_node;

        protected override BehaviorNodeBase Node
        {
            get
            {
                return m_node;
            }
        }

        public override BehaviorNodeChainBase Append(BehaviorNodeBase node)
        {
            m_node.Append(node);
            return this;    
        }

        public SequenceNodeChain Fill(GameFrameworkAction onExecuteBegin, GameFrameworkAction onExecuteEnd, params BehaviorNodeBase[] nodes)
        {
            m_node = ReferencePool.Acquire<SequenceNode>().Fill(onExecuteBegin, onExecuteEnd,nodes);
            return this;
        }

        public override void Clear()
        {
            base.Clear();
            
        }
    }
}

