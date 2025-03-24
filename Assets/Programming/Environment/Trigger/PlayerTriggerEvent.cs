using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerEvent : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private bool triggerOnce = false;

	[Header("Events")]
	[SerializeField] private UnityEvent onTriggerEnter;
	[SerializeField] private UnityEvent onTriggerStay;
	[SerializeField] private UnityEvent onTriggerExit;

	private bool _triggered = false;
	
	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player") || triggerOnce && _triggered)
		{
			return;
		}

		_triggered = true;
		onTriggerEnter?.Invoke();
	}

	void OnTriggerStay(Collider other)
	{
		if (!other.gameObject.CompareTag("Player") || triggerOnce && _triggered)
		{
			return;
		}

		_triggered = true;
		onTriggerStay?.Invoke();
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Player") || triggerOnce && _triggered)
		{
			return;
		}

		_triggered = true;
		onTriggerExit?.Invoke();
	}
}
