using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityGameFramework.Runtime;

namespace Trinity
{

    public class TestForm2 : FGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            Log.Info("666");
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameEntry.UI.CloseUIForm(UIForm);
            }
        }

        
    }
}

