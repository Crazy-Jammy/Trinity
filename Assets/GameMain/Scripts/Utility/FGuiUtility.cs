using FairyGUI;
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
        private static Dictionary<string, int> m_FGuiResRef = new Dictionary<string, int>();
        
        /// <summary>
        /// 添加FGui资源包
        /// </summary>
        public static void AddFGuiRes(string packageName)
        {

            if (m_FGuiResRef.ContainsKey(packageName))
            {
                m_FGuiResRef[packageName]++;
                return;
            }

            GameEntry.Resource.LoadAsset($"Assets/GameMain/UI/FGuiRes/{packageName}.prefab", Constant.AssetPriority.UIFormAsset, new LoadAssetCallbacks(
           (assetName, asset, duration, userData) =>
           {
               GameObject fguiRes = (GameObject)asset;
               ReferenceCollector rc = fguiRes.GetComponent<ReferenceCollector>();
               m_FGuiResRef.Add(packageName, 1);
               Log.Info("加载FGuiRes成功，资源名：" + assetName);

               //自定义FGui资源包加载方式
               UIPackage.AddPackage(packageName, (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
               {
                   destroyMethod = DestroyMethod.Unload;
                   return rc.Get<Object>(name);
               });
               
           },

           (assetName, status, errorMessage, userData) =>
           {
               Log.Error($"加载FGuiRes失败，资源名：{assetName}，错误信息：{errorMessage}");
           }));
        }

        /// <summary>
        /// 移除FGui资源包
        /// </summary>
        public static void RemoveFGuiRes(string packageName)
        {
            if (!m_FGuiResRef.ContainsKey(packageName))
            {
                return;
            }

            m_FGuiResRef[packageName]--;
            if (m_FGuiResRef[packageName] <= 0)
            {
                m_FGuiResRef.Remove(packageName);
                UIPackage.RemovePackage(packageName);
            }
        }
    }
}


