using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 30;
    private Queue<GameObject> bulletQueue = new Queue<GameObject>();

    //ʹ�ö���ش洢�ӵ� ����Ƶ�����������ٶ��� ���´�����������
    //ûд�ӵ����˺�����
    void Start() {
        for (int i = 0;i < poolSize;i++) {
            GameObject bullet = NewBullet();
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    //��һ���ӵ�
    public GameObject GetBullet() {
        if (bulletQueue.Count > 0) {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        //�����˾��½�һ���ӵ�
        GameObject newBullet = NewBullet();
        return newBullet;
    }

    //��һ���ӵ�
    public void ReturnBullet(GameObject bullet) {
        bullet.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }

    //�½�һ���ӵ�
    public GameObject NewBullet() {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.parent = transform;
        newBullet.GetComponent<BulletController>().bulletPool = this; //�����ӵ��Ķ����
        return newBullet;
    }
}
