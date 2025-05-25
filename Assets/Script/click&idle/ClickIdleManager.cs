using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class ClickIdleManager : MonoBehaviour
{
    public static ClickIdleManager Instance;

    //倒计时
    private float totalTime = 300f;
    private float currentTime;

    //基础数值
    public float files = 0;
    public float clickValue = 1f;
    public float autoValue = 0;

    //升级数值
    //点击
    public int clickLevel = 1;
    public float clickUpgradeCost = 10f;
    //挂机数量
    public int idleCount = 0;
    public float idleAddCost = 20f;
    //挂机等级
    public int idleLevel = 1;
    public float idleUpgradeCost = 50f;
    //多点击
    public int multiClickLevel = 0;
    public float multiClickUpgradeCost = 2000f;
    //硬盘
    public bool haveDisk = false;
    public int diskCost = 500;
    public float maxDiskNumber = 1000f;
    public float diskNumber = 0;
    //硬盘升级
    public int diskLevel = 1;
    public float diskUpgradeCost = 200f;
    //硬盘自动
    public int diskAutoLevel = 0;
    public float diskAutoUpgradeCost = 1000f;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = totalTime;
        UpdateCountdownDisplay();
    }

    private void UpdateCountdownDisplay()
    {
        currentTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        ClickIdleUI.Instance.countDown.text = $"倒计时：{string.Format("{0:00}:{1:00}", minutes, seconds)}";

        if (currentTime <= 0)
        {
            SceneManager.LoadScene("TowerDefenceScene");
        }
    }

    void FixedUpdate()
    {
        diskBoom();
        AutoGetFiles();
        ClickIdleUI.Instance.filesText.text = $"文件: {files.ToString("F2")}";
        ClickIdleUI.Instance.clickValueText.text = $"点击获取: {((multiClickLevel + 1) * Math.Pow(clickLevel, 1.7)).ToString("F2")}/次";
        ClickIdleUI.Instance.autoValueText.text = $"自动获取: {autoValue.ToString("F2")}/s";
        UpdateCountdownDisplay();

        ClickIdleUI.Instance.clickUpgrade.text = $"点击能力 当前等级：{clickLevel} 升级花费：{clickUpgradeCost.ToString("F2")}";
        ClickIdleUI.Instance.idleUpgrade.text = $"文件自动生成脚本 当前数量：{idleCount} 购买花费：{idleAddCost.ToString("F2")}";
        ClickIdleUI.Instance.idleCountUpgrade.text = $"多脚本运行插件 当前等级：{idleLevel} 升级花费：{idleUpgradeCost.ToString("F2")}";
        ClickIdleUI.Instance.multiClickUpgrade.text = $"多标签浏览器 当前等级：{multiClickLevel} 升级花费：{multiClickUpgradeCost.ToString("F2")}";
        ClickIdleUI.Instance.disk.text = $"硬盘 {(haveDisk ? "已解锁" : "未解锁")} {(haveDisk ? "" : "购买花费：" + diskCost.ToString("F2"))}";
        ClickIdleUI.Instance.diskUpgrade.text = $"精准碎片整理 当前等级：{diskLevel} 升级花费：{diskUpgradeCost.ToString("F2")}";
        ClickIdleUI.Instance.autoDisk.text = $"递归复制脚本 当前等级：{diskAutoLevel} 升级花费：{diskAutoUpgradeCost.ToString("F2")}";
    }

    void AutoGetFiles()
    {
        autoValue = (float)(idleCount * (1 + Math.Pow(idleLevel - 1, 2) / 5));
        files += autoValue * Time.fixedDeltaTime;

        if (haveDisk)
        {
            float autoValue2 = (float)(Math.Pow(diskAutoLevel, 2) / 5);
            diskNumber += autoValue2 * Time.fixedDeltaTime;
        }
    }

    void diskBoom()
    {
        System.Random random = new System.Random();
        if (diskNumber >= maxDiskNumber)
        {
            diskNumber = 0;
            files += (float)(Math.Pow(clickLevel, 1.7) * random.Next(500,1000));
        }
    }
}