using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework;

namespace Trinity
{
    /// <summary>
    /// 技能系统组件
    /// </summary>
    public class SkillSystemComponent : GameFrameworkComponent
    {

        /// <summary>
        /// Buff链表
        /// </summary>
        private LinkedList<BuffBase> m_Buffs = new LinkedList<BuffBase>();

        private void Update()
        {
            LinkedListNode<BuffBase> current = m_Buffs.First;
            while (current != null)
            {
                LinkedListNode<BuffBase> temp = current;
                current.Value.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
                current = temp.Next;
            }
        }

        /// <summary>
        /// 添加Buff
        /// </summary>
        public void AttachBuff(BuffBase buff) {
            m_Buffs.AddLast(buff);
            buff.OnAttach();
        }

        /// <summary>
        /// 解除Buff
        /// </summary>
        public void DetachBuff(BuffBase buff)
        {
            if (m_Buffs.Contains(buff))
            {
                m_Buffs.Remove(buff);
                buff.OnDetach();
                ReferencePool.Release(buff);
            }
        }

        /// <summary>
        /// 获取指定Entity上所有的Buff
        /// </summary>
        public List<BuffBase> GetBuffByEntity(Entity owner)
        {
            List<BuffBase> buffs = new List<BuffBase>();
            foreach (BuffBase buff in m_Buffs)
            {
                if (buff.Owner == owner)
                {
                    buffs.Add(buff);
                }
            }
            return buffs;
        }


    }
}

