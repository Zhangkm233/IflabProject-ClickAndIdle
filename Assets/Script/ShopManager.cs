using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text moneyText;
    public GameObject CannonObject;
    private void Update() {
        moneyText.text = PlayerData.PlayerCoin.ToString();
    }
    public void ExitShop() {
        PlayerData.CurrentGameState = PlayerData.GameState.Playing;
        gameObject.SetActive(false);
    }
    
    public void HpUp() {
        if (PlayerData.HpUpLevel < 0 && PlayerData.PlayerCoin < 30) {
            PlayerData.HpUpLevel++;
            PlayerData.PlayerCoin -= 30;
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("CannonObjects");
            foreach (GameObject cannon in cannons) {
                CannonController controller = cannon.GetComponent<CannonController>();
                if (controller != null) {
                    controller.cannonHp += 50; //每次升级增加10点血量
                }
            }
        }
    }

    public void NewCannon() {
        if (PlayerData.PlayerCoin < 100) {
            Debug.Log("Not enough coins to buy a new cannon.");
            return;
        }
        PlayerData.PlayerCoin -= 100;
        GameObject newCannon = Instantiate(Resources.Load<GameObject>("Prefabs/CannonPrefab"));
        newCannon.transform.position = new Vector3(0,0,0); //设置新炮的位置
        Debug.Log("New cannon purchased and instantiated.");
    }
