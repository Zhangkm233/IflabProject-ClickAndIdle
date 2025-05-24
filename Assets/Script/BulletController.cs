using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletPool bulletPool; 
    private double bulletDamage = 10;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("EnemyObject")) {
            EnemyObject enemy = collision.gameObject.GetComponent<EnemyObject>();
            if (enemy != null) {
                enemy.getHurt(bulletDamage);
            }
            ReturnToPool();
        }
    }

    private void ReturnToPool() {
        bulletPool.ReturnBullet(gameObject); 
    }
}
