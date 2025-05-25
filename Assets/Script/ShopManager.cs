using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text moneyText;
    public GameObject CannonObject;
    public GameObject enemySpawner;
    public Canvas shop;
    private void Update() {
        moneyText.text = "Money:" + PlayerData.PlayerCoin.ToString();
    }
    public void ExitShop() {
        PlayerData.CurrentGameState = PlayerData.GameState.Playing;
        shop.gameObject.SetActive(false);
        enemySpawner.GetComponent<EnemySpawner>().StartSpawn();
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
        GameObject newCannon = Instantiate(CannonObject);
        newCannon.transform.position = new Vector3(0,0,0); //设置新炮的位置
    }
}
