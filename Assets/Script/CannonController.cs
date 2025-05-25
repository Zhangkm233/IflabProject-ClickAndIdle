using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CannonController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;//弹速
    //firepoint可以实现炮管之类的东西
    public Transform firePoint;
    public bool isAutoFire = false; //是否自动射击
    public float fireRate = 0.2f; //射速
    public float autoFireRate = 0.8f; //自动射击的射速
    public Transform autoFireTarget; //自动射击的目标 测试用
    public Sprite autoCannonSprite;
    public Sprite autoBarrelSprite;
    public Sprite manualCannonSprite;
    public Sprite manualBarrelSprite;
    private bool isDragging = false; //是否正在拖动
    private bool isHovering = false; //是否鼠标悬停在上面
    private float nextFireTime = 0f;
    private BulletPool bulletPool;
    private float searchRadius = 7f; //搜索半径
    public float cannonHp = 100f; //大炮血量
    public float maxHp;
    public Image HPBAR;
    void Start() {
        bulletPool = GetComponent<BulletPool>();
        UpdateSprite();
        maxHp = cannonHp;
        
        float percent = - (float)(1.315 * (1-(cannonHp / maxHp)));
        RectTransform rt = HPBAR.GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(percent, rt.offsetMax.y);
    }

    void Update()
    {
        
        float percent = - (float)(1.315 * (1-(cannonHp / maxHp)));
        RectTransform rt = HPBAR.GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(percent, rt.offsetMax.y);

        if (PlayerData.CurrentGameState != PlayerData.GameState.Playing) return;
        if (isAutoFire)
        {
            if (autoFireTarget != null)
            {
                firePoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, autoFireTarget.position - firePoint.position);
                AutoFire();
            }
            else
            {
                SearchEnemy();
            }
        }
        else
        {
            if (isDragging) return;
            if (isHovering) return;
            firePoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
            ManualFire();
        }
    }

    private void OnMouseDrag() {
        isDragging = true;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x,transform.position.y,0f);
    }

    private void OnMouseUp() {
        isDragging = false;
    }

    private void OnMouseOver() {
        isHovering = true;
        if (Input.GetMouseButtonDown(1)) { //右键切换自动射击
            ChangeAuto();
        }
    }
    private void OnMouseExit() {
        isHovering = false;
    }

    void ChangeAuto() {
        isAutoFire = !isAutoFire;
        UpdateSprite();
    }

    void UpdateSprite() {
        if (isAutoFire) {
            GetComponent<SpriteRenderer>().sprite = autoCannonSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = autoBarrelSprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = manualCannonSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = manualBarrelSprite;
        }
    }

    void SearchEnemy() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(firePoint.position,searchRadius);
        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag("EnemyObject")) {
                autoFireTarget = collider.transform;
                return; //找到一个敌人就返回
            }
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
        bullet.transform.position = firePoint.position + (firePoint.up * 1f);
        bullet.transform.rotation = firePoint.rotation;
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) {
            Vector2 direction = autoFireTarget.position - firePoint.position;
            direction.Normalize();
            rb.angularVelocity = 0f;
            rb.AddForce(direction * bulletSpeed,ForceMode2D.Impulse);
        }
        StartCoroutine(ReturnBulletAfterDelay(bullet,3f));
    }

    [System.Obsolete]
    void ToPointShoot() {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position + (firePoint.up * 1f);

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        direction.Normalize();

        // 设置子弹朝向射击方向
        bullet.transform.right = direction;
        bullet.transform.Rotate(0,0,-90f);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.angularVelocity = 0f;
            rb.velocity = Vector2.zero; // 清除可能存在的初始速度
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            
            // 可选：锁定旋转
            rb.freezeRotation = true;
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