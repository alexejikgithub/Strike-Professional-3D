using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour, IPoolObject
{

	[SerializeField] private Vector3 _offset;
	[SerializeField] private Image _bar;
	[SerializeField] private float _lerpTime = 0.5f;

	private Transform _target;
	private ObjectPoolController _pool;

	private Coroutine _fillBarCoroutine;

	private void LateUpdate()
	{
		transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
		transform.localScale = Vector3.one / Vector3.Distance(_target.position, Camera.main.transform.position) * 10;
	}

	public void SetTarget(Transform target)
	{
		_target = target;
	}

	public void RestoreFillAmount()
	{
		_bar.fillAmount = 1f;
	}

	public void SetFillAmount(float amount)
	{
		if(!gameObject.activeSelf)
		{
			return;
		}

		if (_fillBarCoroutine != null)
		{
			StopCoroutine(_fillBarCoroutine);
		}
		_fillBarCoroutine = StartCoroutine(FillAmountLerp(amount));
	}

	private IEnumerator FillAmountLerp(float amount)
	{
		float elapsedTime = 0;

		while (elapsedTime < _lerpTime)
		{
			_bar.fillAmount = Mathf.Lerp(_bar.fillAmount, amount, elapsedTime);
			elapsedTime += Time.deltaTime;

			yield return null;
		}
		if (amount <= 0)
		{
			RemoveObject();
		}

	}

	public void SetPool(ObjectPoolController pool)
	{
		_pool = pool;
	}


	public void RemoveObject()
	{
		StopAllCoroutines();
		if (_pool != null)
		{
			_pool.ReturnPooledGameObject(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
