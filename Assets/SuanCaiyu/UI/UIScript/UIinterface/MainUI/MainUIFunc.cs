using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common.UI
{
    public class MainUIFunc : MonoBehaviour
    {
        private StatuValueControl hpControl;
        private StatuValueControl mpControl;
        private StatuValueControl bossControl;
        private FadeUI bossFadeUI;
        private Emotionalbar emotionalbar;
        private ImageCycler rightWeapon;
        private ImageCycler leftWeapon;
        private ImageCycler upWeapon;
        private ImageCycler downWeapon;

        //public Emotionalbar emotionalbar;

        private void Start()
        {
            Transform hp = transform.Find("FirstUI/状态栏/HP");
             hpControl = hp.GetComponent<StatuValueControl>();
            Debug.Log("找到HP控制！");

            Transform mp = transform.Find("FirstUI/状态栏/MP");
             mpControl = mp.GetComponent<StatuValueControl>();
            Debug.Log("找到MP控制！");

            Transform boss = transform.Find("FirstUI/BossHp");
            bossControl = boss.GetComponent<StatuValueControl>();
            bossFadeUI = boss.GetComponent<FadeUI>();
            Debug.Log("找到Boss血条控制和显隐控制！");

            Transform em = transform.Find("FirstUI/情感条");
            emotionalbar = em.GetComponent<Emotionalbar>();
            Debug.Log("找到情感值控制！");

            Transform rw = transform.Find("FirstUI/装备栏/右手武器");
            rightWeapon = em.GetComponent<ImageCycler>();
            Debug.Log("找到右手武器！");

            Transform lw = transform.Find("FirstUI/装备栏/左手武器");
            leftWeapon = em.GetComponent<ImageCycler>();
            Debug.Log("找到左手武器！");

            Transform uw = transform.Find("FirstUI/装备栏/技能");
            upWeapon = em.GetComponent<ImageCycler>();
            Debug.Log("找到技能！");

            Transform dw = transform.Find("FirstUI/装备栏/道具");
            downWeapon = em.GetComponent<ImageCycler>();
            Debug.Log("找到道具！");


        }

        #region 玩家HP方法
        /// <summary>
        /// 增加玩家血量
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePlayerHp(float amount)
        {
            hpControl.Increse(amount);
        }

        /// <summary>
        /// 减少玩家血量
        /// </summary>
        /// <param name="amount"></param>
        public void DecreasePlayerHp(float amount)
        {
            hpControl.Decrese(amount);
        }

        /// <summary>
        /// 设置玩家血量
        /// </summary>
        /// <param name="amount"></param>
        public void SetPlayerHp(float amount)
        {
            hpControl.SetValue(amount);
        }

        /// <summary>
        /// 增加血量上限
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePlayerMaxHp(float amount)
        {
            hpControl.IncreseMaxOnly(amount);
        }

        /// <summary>
        /// 减少玩家血量上限
        /// </summary>
        /// <param name="amount"></param>
        public void DcreasePlyaerMaxHp(float amount)
        {
            hpControl.DecreseMax(amount);
        }

        /// <summary>
        /// 增加玩家血量上限并回满血
        /// </summary>
        /// <param name="amount"></param>
        public void IncresePlyerMaxHpToFull(float amount)
        {
            hpControl.IncreseMax_Full(amount);
        }

        #endregion

        #region 玩家MP方法

        /// <summary>
        /// 增加玩家能量
        /// </summary>
        public void IncreasePlayerMp(float amount)
        {
            mpControl.Increse(amount);
        }

        /// <summary>
        /// 减少玩家能量
        /// </summary>
        /// <param name="amount"></param>
        public void DecreasePlayerMp(float amount)
        {
            mpControl.Decrese(amount);
        }

        /// <summary>
        /// 增加能量上限
        /// </summary>
        /// <param name="amount"></param>
        public void IncreasePlayerMaxMp(float amount)
        {
            mpControl.IncreseMaxOnly(amount);
        }

        /// <summary>
        /// 减少玩家能量上限
        /// </summary>
        /// <param name="amount"></param>
        public void DcreasePlyaerMaxMp(float amount)
        {
            mpControl.DecreseMax(amount);
        }

        /// <summary>
        /// 设置玩家能量
        /// </summary>
        /// <param name="amount"></param>
        public void SetPlayerMp(float amount)
        {
            mpControl.SetValue(amount);
        }

        /// <summary>
        /// 增加玩家能量上限并回满血
        /// </summary>
        /// <param name="amount"></param>
        public void IncresePlyerMaxMpToFull(float amount)
        {
            mpControl.IncreseMax_Full(amount);
        }

        #endregion

        #region 玩家血瓶蓝瓶方法
        #endregion

        #region 玩家BUFF方法
        #endregion

        #region 玩家情感条方法
        /// <summary>
        /// 增加当前情感值
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseEmotion(float amount)
        {
            emotionalbar.IncreaseValue(amount);
        }

        /// <summary>
        /// 减少当前情感值
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseEmotion(float amount)
        {
            emotionalbar.DecreaseValue(amount);
        }

        /// <summary>
        /// 增加当前情感值百分比上限
        /// </summary>
        /// <param name="amount"></param>
        public void IncreseFullEmotion(float amount)
        {
            emotionalbar.IncreseMax(amount);
        }
        
        /// <summary>
        /// 减少当前情感值上限百分比
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseFullEmotion(float amount)
        {
            emotionalbar.DecreaseMax(amount);
        }

        /// <summary>
        /// 更新整个情感条，阈值和最大情感量
        /// </summary>
        /// <param name=""></param>
        public void SetCurrentEmotion(float newlow, float newhigh, float fullamount)
        {
            emotionalbar.ResetEmotionalBar(newlow, newhigh, fullamount);
        }
        #endregion

        #region 玩家切换武器

        /// <summary>
        /// 装备右手武器
        /// </summary>
        public void AddRightWeapon()
        {

        }

        /// <summary>
        /// 切换右手武器
        /// </summary>
        public void SwitchRightWeapon()
        {
            rightWeapon.CycleImage();
        }

        /// <summary>
        /// 装备左手武器
        /// </summary>
        public void AddLeftWeapon()
        {

        }

        /// <summary>
        /// 切换左手武器
        /// </summary>
        public void SwitchLefttWeapon()
        {
            leftWeapon.CycleImage();
        }

        /// <summary>
        /// 装备技能
        /// </summary>
        public void AddUpWeapon()
        {

        }

        /// <summary>
        /// 切换技能
        /// </summary>
        public void SwitchUpWeapon()
        {
            upWeapon.CycleImage();
        }

        /// <summary>
        /// 装备道具
        /// </summary>
        public void AddDownWeapon()
        {

        }

        /// <summary>
        /// 切换道具
        /// </summary>
        public void SwitchDownWeapon()
        {
            downWeapon.CycleImage();
        }
        #endregion

        #region 玩家货币方法
        #endregion

        #region Boss方法
        /// <summary>
        /// 增加BOSS血量，BOSS回血时
        /// </summary>
        /// <param name="amount"></param>
        public void IncreseBossHp(float amount)
        {
            bossControl.IncreseBossHp(amount);
        }

        /// <summary>
        /// 减少BOSS血量，BOSS受击
        /// </summary>
        /// <param name="amount"></param>
        public void DecreseBossHp(float amount)
        {
            bossControl.DecreseBossHP(amount);
        }

        /// <summary>
        /// 设置BOSS满血血量，触发BOSS战时调用
        /// </summary>
        /// <param name="amount"></param>
        public void SetBossHp(float amount)
        {
            bossControl.SetBossHP(amount);
        }

        /// <summary>
        /// BOSS血条出现，触发BOSS战后调用
        /// </summary>
        public void BossHp_On()
        {
            bossFadeUI.FadeIn();
        }

        /// <summary>
        /// BOSS血条消失，离开BOSS战后调用
        /// </summary>
        public void BossHp_Off()
        {
            bossFadeUI.FadeOut();
        }
        #endregion
    }
}
