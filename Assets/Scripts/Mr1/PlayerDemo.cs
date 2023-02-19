using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mr1
{
	public class PlayerDemo : MonoBehaviour
	{
		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal PlayerDemo _this;

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

			public _Start_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(2f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.transform.FollowPath("First", 10f, FollowType.Loop, FollowDirection.Forward).Duration(3f).LookForward(true);
					this._current = new WaitForSeconds(4f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.transform.FollowPath("First", 20f, FollowType.PingPong, FollowDirection.Backward).Flip(true);
					this._current = new WaitForSeconds(5f);
					if (!this._disposing)
					{
						this._PC = 3;
					}
					return true;
				case 3u:
					this._this.transform.FollowPath("Second", 13f, FollowType.PingPong, FollowDirection.Forward).SmoothLookForward(true, 10f);
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

		private void Awake()
		{
			base.transform.FollowPath("First", 10f, FollowType.Loop, FollowDirection.Forward).Log(true);
		}

		private IEnumerator Start()
		{
			PlayerDemo._Start_c__Iterator0 _Start_c__Iterator = new PlayerDemo._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}
	}
}
