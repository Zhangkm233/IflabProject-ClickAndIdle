using UnityEngine;
using System.Collections;

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
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.y < -Camera.main.orthographicSize - 1f) {

            //System.Random random = new System.Random();
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("CannonObject");
            float randomNum = Random.Range(0,cannons.Length);
            GameObject randomCannon = cannons[(int)randomNum];
            Debug.Log("Random Cannon: " + randomCannon.name);
            //float health = randomCannon.GetComponent<CannonController>().cannonHp;
            if (randomCannon != null)
            {
                Debug.Log("hit");
                randomCannon.GetComponent<CannonController>().cannonHp -= Random.Range(10f,25f);
            }

            this.GetComponent<EnemyObject>().DestroySelf();
        }
    }
}