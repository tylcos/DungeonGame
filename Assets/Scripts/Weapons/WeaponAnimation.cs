﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : EnemyAnimation {

    private bool formationComplete = false;
    private bool inRange = true;
    private bool attackComplete = true;

    // Use this for initialization
    void Start () {
        meleeEnemyAccessor = meleeEnemy.GetComponent<MeleeEnemyController>();
	}

    // Update is called once per frame
    void Update()
    {
        /*
        if (meleeEnemyAccessor.isAggro == true)
        {
            animator.SetBool("PlayerInRange", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Formation"))
        {
            animator.SetBool("FormationComplete", true);
            if (meleeEnemyAccessor.isAggro == true)
            {
                animator.SetBool("Shot", true);
            }
        }
        else
        {
            animator.SetBool("FormationComplete", false);
            animator.SetBool("Shot", false);

        }

        if (meleeEnemyAccessor.isAggro == false)
        {
            animator.SetBool("PlayerInRange", false);
        }*/



        if (meleeEnemyAccessor.isAggro == true)
        {
            animator.SetBool("PlayerInRange", true);
            inRange = true;
        }
        else
        {
            animator.SetBool("PlayerInRange", false);
            animator.SetBool("Formation", false);
            animator.SetBool("Shot", false);
            inRange = false;
        }

        if (inRange)
        {

            animator.SetBool("Formation", true);
            formationComplete = true;
        }
        if (formationComplete)
        {
            animator.SetBool("Shot", true);
            attackComplete = true;
        }
    }   
}