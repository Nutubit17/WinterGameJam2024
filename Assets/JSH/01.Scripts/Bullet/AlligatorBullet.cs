using DG.Tweening;
using LJS.Bullets;
using LJS.Enemys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class AlligatorBullet : Bullet
{
    private float _whenEat = 3f;
    private float _currentTime;

    [SerializeField] private Transform _child1;
    [SerializeField] private Transform _child2;
    public override void Update()
    {
        base.Update();

        if (_currentTime >= _whenEat)
        {
            Bite();
            _currentTime = 0f;
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    private void Bite()
    {
        _child1.transform.DORotate(new Vector3(0, 0, 20), 1f).OnComplete(() =>
        _child1.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutExpo));
        _child2.transform.DORotate(new Vector3(0, 0, -20), 1f).OnComplete(() =>
        _child2.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutExpo));
    }

    public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir, float fontSize = 0.2f)
    {
        base.SetBullet(info, owner, RotateToTarget, dir, fontSize);
        _child1 = transform.GetChild(0);
        _child2 = transform.GetChild(1);
    }
}
