using System.Collections.Generic;
using UnityEngine;

public class BulletMagazine : MonoBehaviour
{
    // Object pooling, using Lifo data structure to limit bullets in scene
    public GameObject bulletPrefab;

    public int MagazineSize = 20;
    private Queue<GameObject> bullets = new Queue<GameObject>();
    public void Awake()
    {
        for (int i = 0; i < MagazineSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count == 0)
            return null;

        GameObject bullet = bullets.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
