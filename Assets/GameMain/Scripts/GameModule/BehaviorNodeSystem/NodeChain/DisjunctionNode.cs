using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    /// <summary>
    /// 析取结点（会逐个执行子结点，直到有结点执行完毕）
    /// </summary>
    public class DisjunctionNode : BehaviorNodeBase, IBehaviorNodeChain
    {
        public IBehaviorNodeChain Append(BehaviorNodeBase node)
        {
            throw new System.NotImplementedException();
        }
    }

}
