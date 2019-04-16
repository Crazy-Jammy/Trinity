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
        IBehaviorNodeChain node;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Log.Info("进入了测试流程");

            

        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameEntry.BehaviorNodeSystem.RemoveNode(node as BehaviorNodeBase);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                node = GameEntry.BehaviorNodeSystem.Sequence();

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
}

