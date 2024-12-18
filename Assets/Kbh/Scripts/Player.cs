using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Required Settings")]
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerInput Input { get; private set; }

    [field: Header("Collision Checks")]
    [field: SerializeField] public HpCollision EnemyDetector { get; private set; }
    [field: SerializeField] public EntityCollision WallDetector { get; private set; }

    [field: Header("Visual settings")]
    [field: SerializeField] public EntityVisual VisualComponent { get; private set; }

    [SerializeField] private GhostController _controller;

    [Header("Feedbacks")]
    [SerializeField] private PlayerHitFeedback _hitFeedback;
    [SerializeField] private PlayerDieFeedback _dieFeedback;



    private void Awake()
    {
        EnemyDetector.Init();
        Movement.Init();
        Input.Init();

        Input.OnMoveEvent += HandleOnMoveEvent;
        Input.OnDashEvent += HandleOnDashEvent;

        _hitFeedback.Init(this);
        _dieFeedback.Init(this);
    }

    private void OnDestroy()
    {
        Input.OnMoveEvent -= HandleOnMoveEvent;
        Input.OnDashEvent -= HandleOnDashEvent;
    }

    private void Update()
    {
        Movement.Animation();
    }

    private void FixedUpdate()
    {
        if (!Movement.IsDash) // dash 중에는 enemy 무시
            EnemyDetector.Check();

        WallDetector.Check();
    }


    private void HandleOnMoveEvent(Vector2 dir)
    {
        Movement.Move(dir);
    }

    private void HandleOnDashEvent()
    {
        _controller.enabled = true;
        Movement.StartDash();
        Input.DisableDash();

        Movement.Update();

        DOVirtual.DelayedCall(Movement.DashTime, () =>
        {
            _controller.enabled = false;
            Movement.EndDash();
            Input.EnableDash();
        });
    }

}
