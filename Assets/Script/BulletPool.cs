using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 30;
    private Queue<GameObject> bulletQueue = new Queue<GameObject>();

    //使用对象池存储子弹 避免频繁创建和销毁对象 导致大量消耗性能
    //没写子弹的伤害代码
    void Start() {
        for (int i = 0;i < poolSize;i++) {
            GameObject bullet = NewBullet();
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    //拿一个子弹
    public GameObject GetBullet() {
        if (bulletQueue.Count > 0) {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        //不够了就新建一个子弹
        GameObject newBullet = NewBullet();
        return newBullet;
    }

    //回一个子弹
    public void ReturnBullet(GameObject bullet) {
        bullet.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }

    //新建一个子弹
    public GameObject NewBullet() {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.parent = transform;
        newBullet.GetComponent<BulletController>().bulletPool = this; //设置子弹的对象池
        return newBullet;
    }
}
