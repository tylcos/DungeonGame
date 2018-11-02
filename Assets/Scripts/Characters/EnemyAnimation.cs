﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

    public Animator animator;
    [HideInInspector]
    public GameObject meleeEnemy;
    public MeleeEnemyController meleeEnemyAccessor;

    private bool formationComplete = false;

    // Use this for initialization
    void Start () {
        meleeEnemyAccessor = meleeEnemy.GetComponent<MeleeEnemyController>();
    }
	
	// Update is called once per frame
	void Update () {

        /*if (meleeEnemyAccessor.isAggro == true)
        {
            animator.SetBool("PlayerInRange", true);
        }
        else
        {
            animator.SetBool("PlayerInRange", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Formation_Animation"))
        {
            animator.SetBool("FormationComplete", true);
            if(meleeEnemyAccessor.isAggro == true)
            {
                animator.SetBool("AttackComplete", true);
            }
        }*/

        if (meleeEnemyAccessor.isAggro == true)
        {
            animator.SetBool("PlayerInRange", true);
            animator.SetBool("Formation", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Formation_Animation"))
            {
                formationComplete = true;
            }
            if (formationComplete == true)
            {
                animator.SetBool("Attack", true);
            }
        }
        if(meleeEnemyAccessor.isAggro == false)
        {
            animator.SetBool("PlayerInRange", false);
            animator.SetBool("Formation", false);
            animator.SetBool("Attack", true);
        }
    }
}
