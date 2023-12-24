using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public enum  UIType
        {
            // 经验值，血量，杀敌数，等级，时间
            Exp,Health,KillCount,PlayerLevel,Time
        }

        public UIType Type;

        public Slider MySlider;
        private void Update()
        {
            switch (Type)
            {
                case UIType.Exp:
                    var curPlayerLevel = GameManager.Instance.CurLevel;
                    MySlider.value = GameManager.Instance.PlayerExp /
                                     GameManager.Instance.PlayerLevelUpExpTable[GameManager.Instance.CurLevel - 1];
                    break;
                case UIType.Health:
                    break;
                case UIType.KillCount:
                    break;
                case UIType.PlayerLevel:
                    break;
                case UIType.Time:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
