using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform attack;
    public Animator animator;
    public LayerMask playerLayer;

    public int damage = 20;

    public float attackRange = 0.5f;
    public float deathTime;

    public int maxHP = 100;
    int actualHP;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        actualHP = maxHP;
        getDeathTime();
    }

    void Update()
    {
            Attack();
    }

    public void getDeathTime()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == "death"){
                deathTime = clip.length;
            }
        }
    }

    public void TakeDamage(int damage){
        actualHP -= damage;
        animator.SetTrigger("hit");
        if(actualHP <= 0){
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            Die();
        }
    }

    void Attack(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack.position, attackRange, playerLayer);

        foreach(Collider2D player in hitPlayer){
            player.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }

    void Die(){
        animator.SetBool("isDead", true);
        StartCoroutine(WaitDeath());
    }

    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(deathTime);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected(){
        if(attack == null){
            return;
        }

        Gizmos.DrawWireSphere(attack.position, attackRange);
    }

}