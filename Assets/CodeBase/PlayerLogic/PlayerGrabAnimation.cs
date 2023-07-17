using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DitzelGames.FastIK;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
  public class PlayerGrabAnimation : MonoBehaviour
  {
    public bool IsGrabbing { get; private set; }
    
    [SerializeField] private PlayerAnimator PlayerAnimator;
    [SerializeField] private FastIKFabric FastIK;
    [SerializeField] private Transform FastIKTarget;
    [SerializeField] private Transform RightHand;
    [SerializeField] private Transform ObjInHandPos;
    [SerializeField] private Transform Basket;
    [SerializeField] private AnimationCurve YCurveToBasket;

    [SerializeField] private float TimeToTarget;
    [SerializeField] private float TimeToBasket;
    [SerializeField] private float TimeToIdle;
    [SerializeField] private float TimeToAlign;

    [SerializeField] private Transform Target;
    
    private Vector3 _idleHandPos;

    private void Awake()
    {
      _idleHandPos = RightHand.position;
      ResetHandPosition();
      DisableIK();
    }
    
    [ContextMenu("Grab")]
    private void a()
    {
      Grab(Target).Forget();
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
      
      //await ObjToBasket(objTransform);

      ResetHandPosition();
      DisableIK();
      IsGrabbing = false;
    }

    private async Task HandToObj(Transform objTransform)
    {
      Vector3 startPos = FastIKTarget.transform.position;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToTarget)
      {
        FastIKTarget.transform.position = Vector3.Slerp(startPos, objTransform.transform.position, t);
        await UniTask.Yield();
      }

      FastIKTarget.transform.position = objTransform.transform.position;
    }

    private async Task HandToIdle()
    {
      Vector3 startPos = FastIKTarget.transform.position;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToIdle)
      {
        FastIKTarget.transform.position = Vector3.Slerp(startPos, _idleHandPos, t);
        await UniTask.Yield();
      }
    }

    private async Task HandToBasket()
    {
      Vector3 startPos = FastIKTarget.transform.position;
      for (float t = 0; t < 1; t += (Time.deltaTime / TimeToBasket))
      {
        Vector3 newPos = Vector3.Slerp(startPos, Basket.transform.position, t);
        newPos += YCurveToBasket.Evaluate(t) * Vector3.up;
        FastIKTarget.transform.position = newPos;
        await UniTask.Yield();
      }
      
      FastIKTarget.transform.position = Basket.transform.position;
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

    private async Task ObjToBasket(Transform objTransform)
    {
      Vector3 startPos = objTransform.position;
      for (float t = 0; t < 1; t += Time.deltaTime / TimeToAlign)
      {
        objTransform.position = Vector3.Slerp(startPos, Basket.position, t);
        await UniTask.Yield();
      }

      objTransform.position = Basket.position;
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
    }
  }
}