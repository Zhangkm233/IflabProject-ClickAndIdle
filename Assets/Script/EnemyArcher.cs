using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    private float speed;
    private Vector2 moveDirection = Vector2.down;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    void Attack() {
        //��ûд��
        BulletPool bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
        GameObject bullet = bulletPool.GetBullet();
    }

    void Update() {
        // �����ƶ�
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // ����Ƴ���Ļ�ײ������ٶ���
        if (transform.position.y < -Camera.main.orthographicSize - 1f) {
            this.GetComponent<EnemyObject>().DestroySelf();
        }
    }
}
