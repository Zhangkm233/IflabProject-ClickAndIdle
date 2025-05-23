using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletPool bulletPool; //�ӵ���
    private double bulletDamage = 10;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyObject enemy = collision.gameObject.GetComponent<EnemyObject>();
            if (enemy != null) {
                enemy.getHurt(bulletDamage);
            }
            ReturnToPool();
        }
    }

    private void ReturnToPool() {
        bulletPool.ReturnBullet(gameObject); //���ӵ����ص�����
    }
}
