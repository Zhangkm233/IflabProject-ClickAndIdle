using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    private float speed;
    private Vector2 moveDirection = Vector2.down;
    private GameObject[] cannons;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update() {
        // 向下移动
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // 如果移出屏幕底部，销毁对象
        if (transform.position.y < -Camera.main.orthographicSize - 1f)
        {
            this.GetComponent<EnemyObject>().DestroySelf();

            cannons = GameObject.FindGameObjectsWithTag("CannonObject");
            
            System.Random random = new System.Random();
            GameObject randomCannon = cannons[random.Next(0, cannons.Length)];

            float health = randomCannon.GetComponent<CannonController>().cannonHp;
            if (health != null)
            {
                health -= random.Next(1, 10);
            }
        }
    }
}