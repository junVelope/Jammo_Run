using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public float Speed;
    public CharacterController controller;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;


    private Vector3 moveVector;

    public float minMoveSpeed = 5f;
    public float maxMoveSpeed = 20f;
    public float accelMoveSpeed = 0.8f;

    float speedReciprocal;
    float currentMoveSpeed;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
        currentMoveSpeed = minMoveSpeed;
        speedReciprocal = 1 / maxMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat ("Blend", Speed, StartAnimTime, Time.deltaTime);

        float accelEase = (maxMoveSpeed - currentMoveSpeed) * speedReciprocal;
        currentMoveSpeed += accelMoveSpeed * accelEase * Time.fixedDeltaTime;

        transform.Translate(0, 0, currentMoveSpeed * Time.deltaTime);
    }
}
