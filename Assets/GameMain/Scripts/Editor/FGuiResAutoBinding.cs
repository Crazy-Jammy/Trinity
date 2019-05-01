using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Trinity.Editor
{
    /// <summary>
    /// FGui资源自动绑定
    /// </summary>
    public class FGuiResAutoBinding
    {
        private const string FGuiResPrefabPath = "Assets/GameMain/UI/FGuiRes/";

        [MenuItem("Trinity/自动绑定FGui资源")]
        public static void AutoBindingFGuiRes()
        {
            ClearResPrefabs();

            StartBindingFGuiRes<Texture2D>("*.png");
            StartBindingFGuiRes<TextAsset>("*.bytes");
            StartBindingFGuiRes<AudioClip>("*.wav");
            StartBindingFGuiRes<AudioClip>("*.mp3");

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("完成", "已经绑定完成", "关闭");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 清理资源预制体
        /// </summary>
        private static void ClearResPrefabs()
        {
            DirectoryInfo dir = new DirectoryInfo(FGuiResPrefabPath);
            FileInfo[] files = dir.GetFiles("*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = $"{FGuiResPrefabPath}{files[i].Name}";
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                ReferenceCollector rc = prefab.GetComponent<ReferenceCollector>();

                if (rc != null)
                {
                    rc.Clear();
                }

                float process = (float)i / files.Length;
                EditorUtility.DisplayProgressBar("自动绑定", "正在清理FGui资源预制体中...", process);
            }
        }

        /// <summary>
        /// 开始绑定FGui资源
        /// </summary>
        private static void StartBindingFGuiRes<T>(string resSuffix) where T : Object
        {
            if (!Directory.Exists(FGuiResPrefabPath))
            {
                return;
            }

            //获取目录下所有指定后缀的文件
            DirectoryInfo dir = new DirectoryInfo(FGuiResPrefabPath);
            FileInfo[] files = dir.GetFiles(resSuffix, SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                //FGui资源包名称 同时也是资源预制体名称
                string packageName = files[i].Name.Split('_')[0];

                //加载资源文件
                string fileFullPath = "Assets" + files[i].FullName.Split(new string[] { "Assets" }, System.StringSplitOptions.None)[1];
                Object fileObject = AssetDatabase.LoadAssetAtPath<T>(fileFullPath);

                //将资源文件添加到资源预制体的RC中
                GameObject prefab = GetResPrefab($"{FGuiResPrefabPath}{packageName}.prefab");
                ReferenceCollector rc = prefab.GetComponent<ReferenceCollector>();
                rc.Add(fileObject.name, fileObject);

                float process = (float)i / files.Length;
                EditorUtility.DisplayProgressBar("自动绑定", "正在自动绑定中...", process);
            }
        }

        /// <summary>
        /// 获取资源预制体
        /// </summary>
        private static GameObject GetResPrefab(string path)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                string prefabName = Path.GetFileName(path);
                GameObject newPrefab = new GameObject(prefabName);
                newPrefab.AddComponent<ReferenceCollector>();
                PrefabUtility.CreatePrefab(path, newPrefab);
                GameObject.DestroyImmediate(newPrefab);
                prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            }

            return prefab;
        }

    }

    
}

