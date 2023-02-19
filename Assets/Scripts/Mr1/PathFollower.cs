using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mr1
{
	public class PathFollower : MonoBehaviour
	{
		private sealed class _FollowPath_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal PathFollower _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			private static TweenCallback __f__am_cache0;

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

			public _FollowPath_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (this._this.logMessage)
					{
						UnityEngine.Debug.Log(string.Format("[{0}] Follow() Path:{1}, Type:{2}, Speed:{3}", new object[]
						{
							this._this.name,
							this._this.pathData.name,
							this._this.followType,
							this._this.moveSpeed
						}));
					}
					break;
				case 2u:
					break;
				default:
					return false;
				}
				this._this._currentIndex = Mathf.Clamp(this._this._currentIndex, 0, this._this.pathData.linePoints.Count - 1);
				if (this._this.IsOnPoint(this._this._currentIndex))
				{
					if (this._this.IsEndPoint(this._this._currentIndex))
					{
						if (!LoadMap.infoMap.Loop)
						{
							Enemy component = this._this.transform.GetComponent<Enemy>();
							this._this.transform.DOLocalMove(component.posInit, 1f, false).OnComplete(delegate
							{
							});
						}
						this._PC = -1;
						return false;
					}
					this._this._currentIndex = this._this.GetNextIndex(this._this._currentIndex);
				}
				else
				{
					this._this.MoveTo(this._this._currentIndex);
				}
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
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

			private static void __m__0()
			{
			}
		}

		private sealed class _FollowPathToPoint_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int targetIndex;

			internal PathFollower _this;

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

			public _FollowPathToPoint_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (this._this.logMessage)
					{
						UnityEngine.Debug.Log(string.Format("[{0}] FollowToPoint() Path:{1}, Speed:{2}", this._this.name, this._this.pathData.name, this._this.moveSpeed));
					}
					this.targetIndex = Mathf.Clamp(this.targetIndex, 0, this._this.pathData.linePoints.Count - 1);
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (!this._this.IsOnPoint(this.targetIndex))
				{
					this._this._currentIndex = Mathf.Clamp(this._this._currentIndex, 0, this._this.pathData.linePoints.Count - 1);
					if (this._this.IsOnPoint(this._this._currentIndex))
					{
						if (this._this.IsEndPoint(this._this._currentIndex))
						{
							goto IL_193;
						}
						this._this._currentIndex = this._this.GetNextIndex(this._this._currentIndex);
					}
					else
					{
						this._this.MoveTo(this._this._currentIndex);
					}
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				IL_193:
				this._PC = -1;
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

		public bool logMessage;

		public PathData pathData;

		public FollowType followType;

		public FollowDirection followDirection;

		public float moveSpeed = 10f;

		public float rotateSpeed = 10f;

		[SerializeField]
		private bool _flip;

		[SerializeField]
		private bool _lookForward;

		[SerializeField]
		private bool _smoothLookForward;

		private Transform _transform;

		private int _currentIndex;

		public static PathFollower Create(Transform transform)
		{
			PathFollower pathFollower = transform.GetComponent<PathFollower>();
			if (pathFollower == null)
			{
				pathFollower = transform.gameObject.AddComponent<PathFollower>();
			}
			pathFollower._transform = transform;
			return pathFollower;
		}

		public static void Stop(Transform transform)
		{
			PathFollower component = transform.GetComponent<PathFollower>();
			if (component != null)
			{
				component.StopFollowing();
				UnityEngine.Object.Destroy(component);
			}
		}

		public void SetFlip(bool useFlip)
		{
			this._lookForward = (this._smoothLookForward = false);
			this._flip = useFlip;
		}

		public void SetLookForward(bool useLookforward)
		{
			this._flip = (this._smoothLookForward = false);
			this._lookForward = useLookforward;
		}

		public void SetSmoothLookForward(bool useSmoothLookforward, float rotSpeed)
		{
			this._flip = (this._lookForward = false);
			this._smoothLookForward = useSmoothLookforward;
			this.rotateSpeed = rotSpeed;
		}

		public void Follow(PathData pathData, float moveSpeed, FollowType followType, FollowDirection followDirection)
		{
			this.pathData = pathData;
			this.moveSpeed = moveSpeed;
			this.followType = followType;
			this.followDirection = followDirection;
			this.StopFollowing();
			int closestLineIndex = this.GetClosestLineIndex(this._transform.position);
			this._currentIndex = this.GetClosestPointIndex(closestLineIndex * 20, this._transform.position);
			base.StartCoroutine(this.FollowPath());
		}

		public void FollowToPoint(PathData pathData, float moveSpeed, Vector2 targetPos)
		{
			this.pathData = pathData;
			this.moveSpeed = moveSpeed;
			this.followType = FollowType.Once;
			this.StopFollowing();
			int closestLineIndex = this.GetClosestLineIndex(this._transform.position);
			this._currentIndex = this.GetClosestPointIndex(closestLineIndex * 20, this._transform.position);
			closestLineIndex = this.GetClosestLineIndex(targetPos);
			int closestPointIndex = this.GetClosestPointIndex(closestLineIndex * 20, targetPos);
			this.followDirection = ((this._currentIndex >= closestPointIndex) ? FollowDirection.Backward : FollowDirection.Forward);
			base.StartCoroutine(this.FollowPathToPoint(closestPointIndex));
		}

		public void StopFollowing()
		{
			base.StopCoroutine(this.FollowPath());
			base.StopCoroutine("FollowPathToPoint");
		}

		private IEnumerator FollowPath()
		{
			PathFollower._FollowPath_c__Iterator0 _FollowPath_c__Iterator = new PathFollower._FollowPath_c__Iterator0();
			_FollowPath_c__Iterator._this = this;
			return _FollowPath_c__Iterator;
		}

		private IEnumerator FollowPathToPoint(int targetIndex)
		{
			PathFollower._FollowPathToPoint_c__Iterator1 _FollowPathToPoint_c__Iterator = new PathFollower._FollowPathToPoint_c__Iterator1();
			_FollowPathToPoint_c__Iterator.targetIndex = targetIndex;
			_FollowPathToPoint_c__Iterator._this = this;
			return _FollowPathToPoint_c__Iterator;
		}

		private void MoveTo(int pointIndex)
		{
			Vector3 vector = this.pathData.linePoints[pointIndex];
			if (this._flip)
			{
				Vector3 vector2 = vector - this._transform.position;
				this._transform.right = ((vector2.x < 0f) ? Vector3.left : Vector3.right);
			}
			else if (this._lookForward)
			{
				Vector3 up = vector - this._transform.position;
				up.z = 0f;
				this._transform.up = up;
			}
			else if (this._smoothLookForward)
			{
				Vector3 vector3 = vector - this._transform.position;
				vector3.z = 0f;
				this._transform.up = Vector3.Lerp(this._transform.up, vector3.normalized, this.rotateSpeed * Time.smoothDeltaTime);
			}
			this._transform.position = Vector3.MoveTowards(this._transform.position, vector, this.moveSpeed * Time.smoothDeltaTime);
		}

		private bool IsOnPoint(int pointIndex)
		{
			return (this._transform.position - this.pathData.linePoints[pointIndex]).sqrMagnitude < 0.1f;
		}

		private bool IsEndPoint(int pointIndex)
		{
			FollowType followType = this.followType;
			if (followType != FollowType.Once)
			{
				return followType != FollowType.Loop && followType != FollowType.PingPong && false;
			}
			return pointIndex == this.EndIndex();
		}

		private int StartIndex()
		{
			if (this.followDirection == FollowDirection.Forward)
			{
				return 0;
			}
			return this.pathData.linePoints.Count - 1;
		}

		private int EndIndex()
		{
			if (this.followDirection == FollowDirection.Backward)
			{
				return 0;
			}
			return this.pathData.linePoints.Count - 1;
		}

		private int GetNextIndex(int currentIndex)
		{
			int result = -1;
			FollowType followType = this.followType;
			if (followType != FollowType.Once)
			{
				if (followType != FollowType.Loop)
				{
					if (followType == FollowType.PingPong)
					{
						if (this.followDirection == FollowDirection.Forward)
						{
							if (currentIndex < this.EndIndex())
							{
								result = currentIndex + 1;
							}
							else
							{
								this.followDirection = FollowDirection.Backward;
								result = currentIndex - 1;
							}
						}
						else if (this.followDirection == FollowDirection.Backward)
						{
							if (currentIndex > this.EndIndex())
							{
								result = currentIndex - 1;
							}
							else
							{
								this.followDirection = FollowDirection.Forward;
								result = currentIndex + 1;
							}
						}
					}
				}
				else if (this.followDirection == FollowDirection.Forward)
				{
					if (currentIndex < this.EndIndex())
					{
						result = currentIndex + 1;
					}
					else
					{
						result = this.StartIndex();
					}
				}
				else if (this.followDirection == FollowDirection.Backward)
				{
					if (currentIndex > this.EndIndex())
					{
						result = currentIndex - 1;
					}
					else
					{
						result = this.StartIndex();
					}
				}
			}
			else if (this.followDirection == FollowDirection.Forward)
			{
				if (currentIndex < this.EndIndex())
				{
					result = currentIndex + 1;
				}
			}
			else if (this.followDirection == FollowDirection.Backward && currentIndex > this.EndIndex())
			{
				result = currentIndex - 1;
			}
			return result;
		}

		private int GetClosestLineIndex(Vector3 pos)
		{
			List<Vector3> points = this.pathData.points;
			Vector3 a = this.ComputeClosestPointFromPointToLine(pos, points[0], points[1]);
			float num = (a - pos).sqrMagnitude;
			int result = 0;
			for (int i = 1; i < points.Count - 1; i++)
			{
				Vector3 a2 = this.ComputeClosestPointFromPointToLine(pos, points[i], points[i + 1]);
				float sqrMagnitude = (a2 - pos).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					result = i;
				}
			}
			return result;
		}

		private int GetClosestPointIndex(int nStartIndex, Vector3 pos)
		{
			int num = Mathf.Min(nStartIndex + 20, this.pathData.linePoints.Count - 1);
			Vector3 a = this.pathData.linePoints[nStartIndex];
			float num2 = (a - pos).sqrMagnitude;
			int result = nStartIndex;
			for (int i = nStartIndex + 1; i <= num; i++)
			{
				Vector3 a2 = this.pathData.linePoints[i];
				float sqrMagnitude = (a2 - pos).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num2 = sqrMagnitude;
					result = i;
				}
			}
			return result;
		}

		private Vector3 ComputeClosestPointFromPointToLine(Vector3 vPt, Vector3 vLinePt0, Vector3 vLinePt1)
		{
			float num = -Vector3.Dot(vPt - vLinePt0, vLinePt1 - vLinePt0) / Vector3.Dot(vLinePt0 - vLinePt1, vLinePt1 - vLinePt0);
			Vector3 result;
			if (num < 0f)
			{
				result = vLinePt0;
			}
			else if (num > 1f)
			{
				result = vLinePt1;
			}
			else
			{
				result = vLinePt0 + num * (vLinePt1 - vLinePt0);
			}
			return result;
		}
	}
}
