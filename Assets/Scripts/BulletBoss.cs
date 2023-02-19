using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class BulletBoss : MonoBehaviour
{
	private sealed class _DisableItem_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal BulletBoss _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _DisableItem_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => Vector2.Distance(this._this.transform.position, Vector2.zero) > 10f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.gameObject.SetActive(false);
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal bool __m__0()
		{
			return Vector2.Distance(this._this.transform.position, Vector2.zero) > 10f;
		}
	}

	public bool isScale;

	private void Awake()
	{
		EventDispatcher.Instance.RegisterListener(EventID.BulletEnemy, new Action<object>(this.Callback));
	}

	private void Callback(object obj = null)
	{
		base.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.DisableItem());
	}

	private IEnumerator DisableItem()
	{
		BulletBoss._DisableItem_c__Iterator0 _DisableItem_c__Iterator = new BulletBoss._DisableItem_c__Iterator0();
		_DisableItem_c__Iterator._this = this;
		return _DisableItem_c__Iterator;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Player")
		{
			other.transform.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
		}
	}
}
