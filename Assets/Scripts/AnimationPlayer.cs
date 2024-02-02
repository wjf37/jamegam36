using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public AudioClip swingWeapon;
    public AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseAnim(string animName){
        Debug.Log("PlayAnim");//names "basicAttack" "counterSpear" "counterSword" "deflectSpear" "deflectSword"
        spriteRenderer.forceRenderingOff = true;
        audioSource.PlayOneShot(swingWeapon);
        animator.SetTrigger(animName);
        spriteRenderer.forceRenderingOff = false;
    }
}
