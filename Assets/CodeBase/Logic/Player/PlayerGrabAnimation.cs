using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DitzelGames.FastIK;
using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class PlayerGrabAnimation : MonoBehaviour
  {
    public bool IsGrabbing { get; private set; }
    
    [SerializeField] private PlayerAnimator PlayerAnimator;
    [SerializeField] private FastIKFabric FastIK;
    [SerializeField] private Transform FastIKTarget;
    [SerializeField] private Transform RightHand;
    
    [Header("MOVE Values")]
    [Header("To Target")]
    [SerializeField] private Transform ObjInHandPos;
    [SerializeField] private float TimeToTarget;
    [SerializeField] private Vector3 OnTargetRotation;

    [Header("To Basket")]
    [SerializeField] private Transform Basket;
    [SerializeField] private float TimeToBasket;
    [SerializeField] private Vector3 OnBasketRotation;
    [SerializeField] private AnimationCurve YCurveToBasket;
    
    [Header("To Idle")]
    [SerializeField] private float TimeToIdle;
    [SerializeField] private float TimeToAlign;

    [SerializeField] private Transform Target;
    
    private Vector3 _idleHandPos;
    private Vector3 _idleHandRotation;

    private void Awake()
    {
      _idleHandPos = RightHand.position;
      _idleHandRotation = RightHand.localRotation.eulerAngles;
      ResetHandPosition();
      DisableIK();
    }

    public async UniTask Grab(Transform objTransform)
    {
      IsGrabbing = true;
      objTransform.TryGetComponent(out Rigidbody objRb);
      EnableIK();
      
      await HandToObj(objTransform);
      
      await GrabObj(objTransform);

      objRb.isKinematic = true;
      
      await ObjToHand(objTransform);

      await HandToBasket();
      
      await ReleaseObj(objTransform);
      objRb.isKinematic = false;
      
      await HandToIdle();

      ResetHandPosition();
      DisableIK();
      IsGrabbing = false;
    }

    private async Task HandToObj(Transform objTransform)
    {
      Vector3 startPos = FastIKTarget.transform.position;
      Quaternion startRotation = FastIKTarget.transform.localRotation;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToTarget)
      {
        FastIKTarget.transform.position = Vector3.Slerp(startPos, objTransform.transform.position, t);
        FastIKTarget.transform.localRotation = Quaternion.Slerp(startRotation, Quaternion.Euler(OnTargetRotation), t);
        await UniTask.Yield();
      }

      FastIKTarget.transform.position = objTransform.transform.position;
    }

    private async Task HandToBasket()
    {
      Vector3 startPos = FastIKTarget.transform.position;
      Quaternion startRotation = FastIKTarget.transform.localRotation;
      for (float t = 0; t < 1; t += (Time.deltaTime / TimeToBasket))
      {
        Vector3 newPos = Vector3.Slerp(startPos, Basket.transform.position, t);
        newPos += YCurveToBasket.Evaluate(t) * Vector3.up;
        FastIKTarget.transform.position = newPos;
        FastIKTarget.transform.localRotation = Quaternion.Slerp(startRotation, Quaternion.Euler(OnBasketRotation), t);
        await UniTask.Yield();
      }
      
      FastIKTarget.transform.position = Basket.transform.position;
    }

    private async Task HandToIdle()
    {
      Vector3 startPos = FastIKTarget.transform.position;
      Quaternion startRotation = FastIKTarget.transform.localRotation;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToIdle)
      {
        FastIKTarget.transform.position = Vector3.Slerp(startPos, _idleHandPos, t);
        FastIKTarget.transform.localRotation = Quaternion.Slerp(startRotation, Quaternion.Euler(_idleHandRotation), t);
        await UniTask.Yield();
      }
    }

    private async Task ObjToHand(Transform objTransform)
    {
      Vector3 startPos = objTransform.transform.position;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToAlign)
      {
        objTransform.transform.position = Vector3.Slerp(startPos, ObjInHandPos.position, t);
        await UniTask.Yield();
      }

      objTransform.transform.position = ObjInHandPos.position;
    }

    private async Task GrabObj(Transform objTransform)
    {
      PlayerAnimator.SetState(PlayerAnimation.Grab);
      await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
      objTransform.SetParent(RightHand);
    }

    private async Task ReleaseObj(Transform objTransform)
    {
      PlayerAnimator.SetState(PlayerAnimation.Release);
      await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
      objTransform.SetParent(null);
    }

    private void EnableIK()
    {
      FastIK.enabled = true;
    }

    private void DisableIK()
    {
      FastIK.enabled = false;
    }

    private void ResetHandPosition()
    {
      FastIKTarget.position = _idleHandPos;
      FastIKTarget.localRotation = Quaternion.Euler(_idleHandRotation);
    }
  }
}