﻿using UnityEngine;

public class BulletController : MonoBehaviour
{
    [HideInInspector]
    public float lifeTime;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float maxCollisions;
    private float currentPenetration = 0;
    [HideInInspector]
    public MovementController source;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void LateUpdate()
    {
        if (Time.time - startTime >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collEvent)
    {
        if (collEvent.gameObject.GetComponent<MovementController>() != null)
        {
            //collEvent.gameObject.GetComponent<MovementController>().Damage(damage);
            bool killedCharacterHit = collEvent.gameObject.GetComponent<MovementController>().OnHitReceived(source, damage);

            try // Bad bug
            {
                source.OnHitDealt(collEvent.gameObject.GetComponent<MovementController>(), killedCharacterHit);
            }
            catch
            {

            }
        }
        currentPenetration++;
        if (currentPenetration >= maxCollisions)
            Destroy(gameObject);

        // damaging should be in an onhittaken method
        // and then landing a hit could be in an onhitdealt method
        // yeah this needs to be entirely rewritten
    }
}
