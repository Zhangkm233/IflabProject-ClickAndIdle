using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed;
    private Vector2 moveDirection = Vector2.down;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    void Update() {
        // 向下移动
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // 如果移出屏幕底部，销毁对象
        if (transform.position.y < -Camera.main.orthographicSize - 1f) {
            
            this.GetComponent<EnemyObject>().DestroySelf();
        }
    }
}