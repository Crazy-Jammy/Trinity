using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Trinity
{
    public class ProcedureTest : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Log.Info("进入了测试流程");

            BehaviorNodeChainBase node = GameEntry.BehaviorNodeSystem.Sequence();
            node
           .Event(() =>
           {
               Log.Info("执行序列结点链");
           })
           .Repeat(3, GameEntry.BehaviorNodeSystem.Event(() => {
               Log.Info("执行重复结点链");
           }))
           .Event(() =>
           {
               Log.Info("序列结点链执行完毕了");
           }).Begin();

        }

    }
}

