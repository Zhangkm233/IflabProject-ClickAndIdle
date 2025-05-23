using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private double enemyHp;

    public void getHurt(double damage) {
        enemyHp -= damage;
        if (enemyHp <= 0) {
            Destroy(gameObject);
        }
    }
}
