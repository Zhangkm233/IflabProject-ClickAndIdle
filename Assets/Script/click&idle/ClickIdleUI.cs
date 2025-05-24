using UnityEngine;
using UnityEngine.UI;

public class ClickIdleUI : MonoBehaviour
{
    public static ClickIdleUI Instance;
    
    public Text filesText;
    public Text clickValueText;
    public Text autoValueText;

    public Text countDown;

    public Text clickUpgrade;
    public Text idleUpgrade;
    public Text idleCountUpgrade;
    void Awake()
    {
        Instance = this;
    }
}