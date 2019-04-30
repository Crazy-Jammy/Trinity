using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using FairyGUI;
namespace Trinity
{
    /// <summary>
    /// FGui界面
    /// </summary>
    public abstract class FGuiForm : UIFormLogic
    {
       

        protected UIPanel UIPanel
        {
            get;
            private set;
        }

        protected GComponent UI
        {
            get;
            private set;
        }

        /// <summary>
        /// 原始深度
        /// </summary>
        public int OriginalDepth
        {
            get;
            private set;
        }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth
        {
            get
            {
                return UIPanel.sortingOrder;
            }
        }

        /// <summary>
        /// 深度间隔
        /// </summary>
        public const int DepthFactor = 100;

        protected virtual void Awake()
        {
            UIPanel = GetComponent<UIPanel>();
            UI = UIPanel.ui;

            FGuiUtility.AddFGuiRes(UIPanel.packageName);
        }

        protected virtual void OnDestory()
        {
            FGuiUtility.RemoveFGuiRes(UIPanel.packageName);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            OriginalDepth = UIPanel.sortingOrder;
        }

        protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            int oldDepth = Depth;
            base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
            int deltaDepth = FGuiGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
            UIPanel.sortingOrder += deltaDepth;

        }

        
    }
}

