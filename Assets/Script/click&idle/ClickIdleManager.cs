using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    void Awake()
    {
        Instance = this;
        InvokeRepeating("AutoGenerateFiles", 1f, 1f);
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
        AutoGetFiles();
        ClickIdleUI.Instance.filesText.text = $"文件: {files.ToString("F2")}";
        ClickIdleUI.Instance.clickValueText.text = $"点击获取: {Math.Pow(clickLevel, 1.7).ToString("F2")}/次";
        ClickIdleUI.Instance.autoValueText.text = $"自动获取: {autoValue.ToString("F2")}/s";
        UpdateCountdownDisplay();

        ClickIdleUI.Instance.clickUpgrade.text = $"点击能力 当前等级：{clickLevel} 升级花费：{clickUpgradeCost.ToString("F2")}";
        ClickIdleUI.Instance.idleUpgrade.text = $"文件自动生成脚本 当前数量：{idleCount} 购买花费：{idleAddCost.ToString("F2")}";
        ClickIdleUI.Instance.idleCountUpgrade.text = $"多脚本运行插件 当前等级：{idleLevel} 升级花费：{idleUpgradeCost.ToString("F2")}";
    }

    void AutoGetFiles()
    {
        autoValue = (float)(idleCount *(1 + Math.Pow(idleLevel - 1, 2) / 5));
        files += autoValue * Time.fixedDeltaTime;
    }
}