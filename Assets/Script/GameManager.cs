using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shopCanvas;
    private void Start() {
        if (PlayerData.CurrentGameState == PlayerData.GameState.Shopping) {
            shopCanvas.SetActive(true);
        }
    }
}
