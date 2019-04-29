using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Trinity
{
    /// <summary>
    /// FGui工具类
    /// </summary>
    public static class FGuiUtility
    {

        private static ReferenceCollector m_RC;

        /// <summary>
        /// 初始化FGui资源预制体
        /// </summary>
        public static void InitFGuiRes()
        {
            GameEntry.Resource.LoadAsset("Assets/GameMain/UI/FGuiRes/FGuiRes.prefab", Constant.AssetPriority.UIFormAsset, new LoadAssetCallbacks(
            (assetName, asset, duration, userData) =>
             {
                 GameObject fguiRes = (GameObject)asset;
                 m_RC = fguiRes.GetComponent<ReferenceCollector>();
                 Log.Info("加载FGuiRes成功");
             },

            (assetName, status, errorMessage, userData) =>
            {
                Log.Error("加载FGuiRes失败：{0}", errorMessage);
            }));

        }

        /// <summary>
        /// 获取FGui资源对象
        /// </summary>
        /// <returns></returns>
        public static Object GetFGuiResObject(string objectName)
        {
            return m_RC.Get<Object>(objectName);
        }


    }
}


