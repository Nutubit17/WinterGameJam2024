using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
   [field: Header("Required Settings")]
   [field: SerializeField] public EntityStatus Status { get; set; }
   [field: SerializeField] public PlayerMovement Movement { get; private set; }

   [field: SerializeField] public PlayerInput Input { get; private set; }
   
   [field: Header("Collision Checks")]
   [field: SerializeField] public HpCollision EnemyDetector { get; private set; }
   [field: SerializeField] public EntityCollision healDetector { get; private set; }
   [SerializeField] private PlayerHPUI _playerHpUI;


   [field: Header("Visual settings")]
   [field: SerializeField] public EntityVisual VisualComponent { get; private set; }

   [SerializeField] private GhostController _moveEffectController;

   [Header("Feedbacks")]
   [SerializeField] private PlayerHitFeedback _hitFeedback;
   [SerializeField] private PlayerDieFeedback _dieFeedback;
   [SerializeField] private PlayerHPUIFeedback _hpUIFeedback;

   [Header("Etc.")]
   [SerializeField] private PlayerStaminaUI _staminaUI;


   private void Start()
   {
      Status.Init();

      Movement.Init(this);
      Input.Init();

      Input.OnMoveEvent += HandleOnMoveEvent;
      Input.OnDashEvent += HandleOnDashEvent;

      _hitFeedback.Init(this);
      _dieFeedback.Init(this);
      _hpUIFeedback.Init(this);

      EnemyDetector.Init(this);
   }

   private void OnDestroy()
   {
      Input.OnMoveEvent -= HandleOnMoveEvent;
      Input.OnDashEvent -= HandleOnDashEvent;
   }

   private void Update()
   {
      Movement.UpdateByFrame();
      _staminaUI.SetGauge(Status.CurrentStamina / Status.MaxStamina);
   }

   private void FixedUpdate()
   {
      if(healDetector.Check())
      {
         Status.AddHp(1);
         Status.AddStamina(1);
         _playerHpUI.SetHp(Status.CurrentHp);
         _staminaUI.SetGauge(Status.CurrentStamina / Status.MaxStamina);
      }

      EnemyDetector.Check();
      if (!Movement.IsDash) // dash �߿��� enemy ����
      {
            Time.timeScale = 1f;
      }
      else
      {
         Time.timeScale = 0.35f;
      }

      _moveEffectController.enabled = Movement.IsDash;
   }


   private void HandleOnMoveEvent(Vector2 dir)
   {
      Movement.Move(dir * Status.Speed);
   }

   private void HandleOnDashEvent(bool isDash)
   {
      Movement.SetDash(isDash);
   }

}
