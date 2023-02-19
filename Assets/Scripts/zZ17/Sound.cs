using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace zZ17
{
	[RequireComponent(typeof(AudioSource))]
	public class Sound : MonoBehaviour
	{
		private sealed class _DelayDespawn_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Sound _this;

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

			public _DelayDespawn_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this._this.source.clip.length + 0.2f);
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
		}

		protected AudioSource source;

		public void Play(AudioClip clip)
		{
			if (this.source == null)
			{
				this.source = base.gameObject.GetComponent<AudioSource>();
			}
			base.gameObject.name = clip.name;
			this.source.clip = clip;
			this.source.Play();
			base.StartCoroutine(this.DelayDespawn());
		}

		protected IEnumerator DelayDespawn()
		{
			Sound._DelayDespawn_c__Iterator0 _DelayDespawn_c__Iterator = new Sound._DelayDespawn_c__Iterator0();
			_DelayDespawn_c__Iterator._this = this;
			return _DelayDespawn_c__Iterator;
		}
	}
}
