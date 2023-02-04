using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    [Range(0f, 0.5f)]
    public float attackSensorRadius = 0.3f;
    [Range(0f, 0.3f)]
    public float attackAnimationTime = 0.15f;

    private Animator animator;
    private string currentAnimation;
    private Transform sensor;
    private float sensorRadius;
    private Transform attackSensor;
    private bool lastIsRight = true;
    private Transform swordParent;
    private Transform sword;
    private SpriteRenderer swordSprite;
    private Vector2 faceDir; // for sword direction
    private bool isAttacking = false;
    private TrailRenderer trail;
    private LTDescr tween;

    void Start() {
        animator = GetComponent<Animator>();
        sensor = transform.Find("InteractSensor");
        sensorRadius = sensor.transform.localPosition.magnitude;
        attackSensor = transform.Find("attackSensor");
        // attackSensorRadius = attackSensor.transform.localPosition.magnitude;
        swordParent = transform.Find("attackSensor/sword");
        sword = swordParent.GetChild(0);
        swordSprite = sword.GetComponent<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        LeanTween.reset();
    }

    public void Animate(Vector2 dir) {
        swordParent.transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        // we will use horizontal animation unless the movement is vetical only
        if (dir.x != 0) {
            animator.speed = 2;
            if (dir.x > 0) {
                Play("MoveRight");
            } else {
                Play("MoveLeft");
            }
            sensor.transform.localPosition = Vector2.right * Mathf.Sign(dir.x) * sensorRadius;
            lastIsRight = dir.x > 0;
            faceDir = Vector2.right * Mathf.Sign(dir.x);
        } else if (dir.y != 0) {
            // animator.speed = 1;
            animator.speed = 2;
            if (dir.y > 0) {
                Play("MoveUp");
            } else {
                Play("MoveDown");
            }
            sensor.transform.localPosition = Vector2.up * Mathf.Sign(dir.y) * sensorRadius;
            faceDir = Vector2.up * Mathf.Sign(dir.y);
        } else {
            // Play(lastIsRight ? "IdleRight" : "IdleLeft");
            animator.StopPlayback();
            animator.speed = 0;
            // sensor.transform.localPosition = Vector2.down * sensorRadius;
            // special case for direction
            swordParent.transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg);
        }
        attackSensor.transform.localPosition = (dir == Vector2.zero
            ? faceDir
            : new Vector2(GetDirection(dir.x), GetDirection(dir.y))
        ).normalized * attackSensorRadius;
    }

    private void Play(string anim) {
        if (anim == currentAnimation) return;
        animator.Play(anim, 0, 0f);
        currentAnimation = anim;
    }

    private float GetDirection(float num) {
        if (num == 0)
            return num;
        else
            return Mathf.Sign(num);
    }

    public void SwingSword() {
        if (tween != null) {
            tween.callOnCompletes();
            LeanTween.cancel(tween.id);
        }

        trail.enabled = true;
        swordSprite.enabled = true;

        float parentRotation = swordParent.rotation.eulerAngles.z;
        sword.rotation = Quaternion.Euler(Vector3.forward * (parentRotation - 89));

        tween = sword.LeanRotateZ(parentRotation + 89, attackAnimationTime)
            .setOnComplete(() => {
                sword.rotation = sword.parent.rotation;
                trail.enabled = false;
                swordSprite.enabled = false;
            });

        // gameObject.LeanValue(-60, 60, attackAnimationTime)
        //     .setOnUpdate((float t) => {
        //         sword.rotation = sword.parent.rotation * Quaternion.Euler(Vector3.forward * t);
        //     })
        //     .setOnComplete(() => {
        //         sword.rotation = sword.parent.rotation;
        //         trail.enabled = false;
        //     });
    }


}

