using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject Enemy;

    public EnemyController enemyRespawn;

    float xPoint;
    float zPoint;

    void Update()
    {
        if(enemyRespawn.enemyDead)
        {
            enemyRespawn.enemyDead =false;
            StartCoroutine(RespawnEnemy(Enemy));
        }
    }
    IEnumerator RespawnEnemy(GameObject obj)
    {
        xPoint = Random.Range(this.transform.position.x-10,this.transform.position.x+10);
        zPoint = Random.Range(this.transform.position.z-10,this.transform.position.z+10);

        obj.transform.position = new Vector3(xPoint, this.transform.position.y, zPoint);
        Physics.SyncTransforms();
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
    }
}
