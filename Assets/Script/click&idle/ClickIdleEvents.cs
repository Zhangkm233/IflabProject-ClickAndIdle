using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class ClickIdleEvent : MonoBehaviour
{
    public ClickIdleManager manager;

    public void GetFile()
    {
        manager.files += (float)Math.Pow(manager.clickLevel, 1.7);
    }

    public void Upgrade(string name)
    {
        if (name == "click")
        {
            if (manager.files >= manager.clickUpgradeCost)
            {
                manager.files -= manager.clickUpgradeCost;
                manager.clickLevel++;
                manager.clickUpgradeCost += (float)(Math.Pow(manager.clickLevel, 3) / Math.Pow(manager.clickLevel, 1.25) * Math.Pow(manager.clickLevel, 0.25) * 20);
            }
            else StartCoroutine(FlashRed());
        }
        if (name == "idleUp")
        {
            if (manager.files >= manager.idleAddCost)
            {
                manager.files -= manager.idleAddCost;
                manager.idleCount++;
                manager.idleAddCost += (float)(Math.Pow(manager.idleCount + 1, 1.2) / Math.Pow(manager.idleCount + 1, -0.25) * 10);
            }
            else StartCoroutine(FlashRed());
        }
        if (name == "idleAdd")
        {
            if (manager.files >= manager.idleUpgradeCost)
            {
                manager.files -= manager.idleUpgradeCost;
                manager.idleLevel++;
                manager.idleUpgradeCost += (float)(Math.Pow(manager.idleLevel, 3.1) * 40);
            }
            else StartCoroutine(FlashRed());
        }
    }
    
    private IEnumerator FlashRed()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        if (clickedObject == null) yield break;
        
        Button button = clickedObject.GetComponent<Button>();
        if (button == null) yield break;

        Graphic buttonGraphic = button.targetGraphic ?? button.GetComponent<Graphic>();
        if (buttonGraphic == null) yield break;

        Color originalColor = buttonGraphic.color;
        buttonGraphic.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        buttonGraphic.color = originalColor;
    }
}