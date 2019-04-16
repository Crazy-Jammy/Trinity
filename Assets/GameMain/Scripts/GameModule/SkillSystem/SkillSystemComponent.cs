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

        private List<BuffBase> m_TempBuffs = new List<BuffBase>();


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
        /// 附加Buff
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
        public BuffBase[] GetBuffByEntity(Entity owner)
        {
            m_TempBuffs.Clear();
            foreach (BuffBase buff in m_Buffs)
            {
                if (buff.Owner == owner)
                {
                    m_TempBuffs.Add(buff);
                }
            }
            return m_TempBuffs.ToArray();
        }


    }
}

