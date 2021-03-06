﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Turret : Enemy
{
    [Space]
    [SerializeField, Range(1, 10)]
    private int shotsPerRound = 3;

    private Transform cannon;
    private Transform bulletSpawn;

    private Quaternion lastRot;
    private bool firing = false;
    private float firingTimer = 1f;

    override protected void Start ()
    {
        base.Start();

        cannon = transform.FindChild("Cannon");
        bulletSpawn = cannon.FindChild("BulletSpawn");
        
        shotsPerRound = Mathf.Max(1, (int) StageManager.GetDifficulty()*2);
        lastRot = cannon.transform.rotation;
    }

    override protected void Update ()
    {
        base.Update();

        if (State.Current == State.GlobalState.Game)
        {
            if (currentState == EnemyState.Attack)
            {
                Vector3 directionVector = Dragon.dragon.transform.position + transform.position;
                cannon.transform.rotation = Quaternion.Slerp(lastRot, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(directionVector.y, directionVector.x)), Time.deltaTime * 0.2f);
                lastRot = cannon.transform.rotation;

                if (!firing && firingTimer >= 1f)
                {
                    StartCoroutine(FiringCoroutine());
                }

                firingTimer += Time.deltaTime;
            }
        }
    }

    private IEnumerator FiringCoroutine ()
    {
        firing = true;

        for (int i = 0; i < shotsPerRound; i++)
        {
            //GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
            //bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward * 100f, ForceMode2D.Force);

      //      cannon.transform.DOScale(1.85f, 0.2f);

       //     yield return new WaitForSeconds(0.2f);

         //   cannon.transform.DOScale(1.75f, 0);

            GameObject bullet = ObjectPool.CreateEnemyBullet(bulletSpawn.position, bulletSpawn.rotation, 400f);

            if (bulletSpawn.right.x <= 0)
            {
                bullet.transform.parent = StageManager.Stage.transform;
            }
            else
            {
                bullet.transform.parent = null;
            }

            AudioManager.PlayClip("primaryFire", true);
        }

        firing = false;
        firingTimer = 0f;

        yield return null;
    }
}
