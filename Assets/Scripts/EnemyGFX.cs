using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private GameObject enemyObject;

    // Update is called once per frame
    void Update()
    {
        if (aiPath)
        {
            if (aiPath.desiredVelocity.x >= 0.01)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= 0.01)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void enemyDead()
    {
        Debug.Log("Bird died at: " + Time.time);
        StartCoroutine(DisableEnemy());
    }

    // Wait for end of frame before disabling enemy object
    private IEnumerator DisableEnemy()
    {
        yield return new WaitForEndOfFrame();
        enemyObject.SetActive(false);
    }
}
