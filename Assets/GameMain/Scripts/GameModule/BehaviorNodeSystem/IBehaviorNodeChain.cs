

namespace Trinity
{
    /// <summary>
    /// 行为结点链接口
    /// </summary>
    public interface IBehaviorNodeChain
    {

        /// <summary>
        /// 追加结点到结点链中
        /// </summary>
        IBehaviorNodeChain Append(BehaviorNodeBase node);
    }
}

