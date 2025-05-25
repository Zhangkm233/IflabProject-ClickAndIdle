using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void ExitShop() {
        PlayerData.CurrentGameState = PlayerData.GameState.Playing;
        gameObject.SetActive(false);
    }

}
