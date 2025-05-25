using UnityEngine;
using UnityEngine.UI;

public class ClickIdleUI : MonoBehaviour
{
    public ClickIdleManager manager;
    public static ClickIdleUI Instance;

    public Image diskBlock;
    public Image progress;

    public Text filesText;
    public Text clickValueText;
    public Text autoValueText;

    public Text countDown;

    public Text clickUpgrade;
    public Text idleUpgrade;
    public Text idleCountUpgrade;
    public Text multiClickUpgrade;
    public Text disk;
    public Text diskUpgrade;
    public Text autoDisk;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (manager.haveDisk) diskBlock.enabled = true;
        else diskBlock.enabled = false;

        float percent = - (178 * (1 - (manager.diskNumber / manager.maxDiskNumber)));
        RectTransform rt = progress.GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(percent, rt.offsetMax.y);
    }
}