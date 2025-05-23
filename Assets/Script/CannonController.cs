using UnityEngine;
using System.Collections;
public class CannonController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    //firepoint可以实现炮管之类的东西
    public Transform firePoint;
    public bool isAutoFire = false; //是否自动射击
    public float fireRate = 0.3f; //射速
    public float autoFireRate = 0.6f; //自动射击的射速
    public Transform autoFireTarget; //自动射击的目标 测试用
    private float nextFireTime = 0f;
    private BulletPool bulletPool;

    void Start() {
        bulletPool = GetComponent<BulletPool>();
    }

    void Update() {
        if(isAutoFire) {
            AutoFire();
        } else {
            ManualFire();
        }
    }

    void AutoFire() {
        if (Time.time >= nextFireTime) {
            //Debug.Log("Fire");
            AutoShoot();
            nextFireTime = Time.time + autoFireRate;
        }
    }

    void ManualFire() {
        if (Input.GetMouseButton(0)) {
            if (Time.time >= nextFireTime) {
                //Debug.Log("Fire");
                ToPointShoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void AutoShoot() {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) {
            Vector2 direction = autoFireTarget.position - firePoint.position;
            direction.Normalize();
            rb.AddForce(direction * bulletSpeed,ForceMode2D.Impulse);
        }
        StartCoroutine(ReturnBulletAfterDelay(bullet,3f));
    }

    void ToPointShoot() {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        //在子弹prefab的rb2d里面调参数来设置子弹的重力之类的东西
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
            direction.Normalize();
            rb.AddForce(direction * bulletSpeed,ForceMode2D.Impulse);
        }
        //等三秒回收子弹
        //装备回收 交易自由
        StartCoroutine(ReturnBulletAfterDelay(bullet,3f));
    }


    IEnumerator ReturnBulletAfterDelay(GameObject bullet,float delay) {
        yield return new WaitForSeconds(delay);
        bulletPool.ReturnBullet(bullet);
    }
}