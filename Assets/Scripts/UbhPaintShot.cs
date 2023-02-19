using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Paint Shot")]
public class UbhPaintShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<List<int>> _paintData___0;

		internal float _paintStartAngle___0;

		internal int _lineCnt___1;

		internal List<int> _line___2;

		internal UbhPaintShot _this;

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

		public _ShotCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this._BulletSpeed <= 0f || this._this._PaintDataText == null)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletSpeed or PaintDataText is not set.");
					return false;
				}
				if (this._this._Shooting)
				{
					return false;
				}
				this._this._Shooting = true;
				this._paintData___0 = this._this.LoadPaintData();
				this._paintStartAngle___0 = this._this._PaintCenterAngle;
				if (0 < this._paintData___0.Count)
				{
					this._paintStartAngle___0 -= ((this._paintData___0[0].Count % 2 != 0) ? (this._this._BetweenAngle * Mathf.Floor((float)this._paintData___0[0].Count / 2f)) : (this._this._BetweenAngle * (float)this._paintData___0[0].Count / 2f + this._this._BetweenAngle / 2f));
				}
				this._lineCnt___1 = 0;
				break;
			case 1u:
				//IL_1B1:
				for (int i = 0; i < this._line___2.Count; i++)
				{
					if (this._line___2[i] == 1)
					{
						UbhBullet bullet = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
						if (bullet == null)
						{
							break;
						}
						float angle = this._paintStartAngle___0 + this._this._BetweenAngle * (float)i;
						this._this.ShotBullet(bullet, this._this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
						this._this.AutoReleaseBulletGameObject(bullet.gameObject);
					}
				}
				this._lineCnt___1++;
				break;
			default:
				return false;
			}
			if (this._lineCnt___1 >= this._paintData___0.Count)
			{
				this._this.FinishedShot();
				this._PC = -1;
			}
			else
			{
				this._line___2 = this._paintData___0[this._lineCnt___1];
				if (0 < this._lineCnt___1 && 0f < this._this._NextLineDelay)
				{
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._this._NextLineDelay));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_1B1;
			}
			return false;

        IL_1B1:
            for (int i = 0; i < this._line___2.Count; i++)
            {
                if (this._line___2[i] == 1)
                {
                    UbhBullet bullet = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
                    if (bullet == null)
                    {
                        break;
                    }
                    float angle = this._paintStartAngle___0 + this._this._BetweenAngle * (float)i;
                    this._this.ShotBullet(bullet, this._this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
                    this._this.AutoReleaseBulletGameObject(bullet.gameObject);
                }
            }
            this._lineCnt___1++;
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

	private static readonly string[] SPLIT_VAL = new string[]
	{
		"\n",
		"\r",
		"\r\n"
	};

	public TextAsset _PaintDataText;

	[Range(0f, 360f)]
	public float _PaintCenterAngle = 180f;

	[Range(0f, 360f)]
	public float _BetweenAngle = 3f;

	public float _NextLineDelay = 0.1f;

	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhPaintShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhPaintShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}

	private List<List<int>> LoadPaintData()
	{
		List<List<int>> list = new List<List<int>>();
		if (string.IsNullOrEmpty(this._PaintDataText.text))
		{
			UnityEngine.Debug.LogWarning("Cannot load paint data because PaintDataText file is empty.");
			return list;
		}
		string[] array = this._PaintDataText.text.Split(UbhPaintShot.SPLIT_VAL, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].StartsWith("#"))
			{
				list.Add(new List<int>());
				for (int j = 0; j < array[i].Length; j++)
				{
					list[list.Count - 1].Add((array[i][j] != '*') ? 0 : 1);
				}
			}
		}
		list.Reverse();
		return list;
	}
}
