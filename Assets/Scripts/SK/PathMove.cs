using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SK
{
	public class PathMove : MonoBehaviour
	{
		private sealed class _FollowPath_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal PathMove _this;

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
					break;
				case 2u:
					break;
				default:
					return false;
				}
				this._this._currentIndex = Mathf.Clamp(this._this._currentIndex, 0, this._this.linePoints.Count - 1);
				if (this._this.IsOnPoint(this._this._currentIndex))
				{
					if (this._this.IsEndPoint(this._this._currentIndex))
					{
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
		}

		private sealed class _FollowPathToPoint_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int targetIndex;

			internal PathMove _this;

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
					this.targetIndex = Mathf.Clamp(this.targetIndex, 0, this._this.linePoints.Count - 1);
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (!this._this.IsOnPoint(this.targetIndex))
				{
					this._this._currentIndex = Mathf.Clamp(this._this._currentIndex, 0, this._this.linePoints.Count - 1);
					if (this._this.IsOnPoint(this._this._currentIndex))
					{
						if (this._this.IsEndPoint(this._this._currentIndex))
						{
							goto IL_13F;
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
				IL_13F:
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

		public List<Vector3> linePoints;

		public List<Vector3> points;

		public FollowType followType;

		public PathLineType lineType;

		public FollowDirection followDirection;

		public float moveSpeed = 10f;

		public float rotateSpeed = 10f;

		private bool _flip;

		private bool _lookForward;

		private bool _smoothLookForward;

		private Transform _transform;

		private int _currentIndex;

		public static float cooltime;

		public static Action onFinished;

		public Vector3 startPoint
		{
			get
			{
				return this.linePoints[0];
			}
		}

		public Vector3 endPoint
		{
			get
			{
				return this.linePoints[this.linePoints.Count - 1];
			}
		}

		private void Start()
		{
			this.linePoints = new List<Vector3>();
			this.SetStraightLine();
		}

		public static void Set(float prCooltime, Action prOnFinished)
		{
			PathMove.cooltime = prCooltime;
			PathMove.onFinished = prOnFinished;
		}

		private void Update()
		{
			PathMove.cooltime = Mathf.Max(0f, PathMove.cooltime - Time.smoothDeltaTime);
			if (PathMove.cooltime <= 0f)
			{
				if (PathMove.onFinished != null)
				{
					PathMove.onFinished();
				}
				UnityEngine.Object.Destroy(this);
			}
		}

		public static PathMove Create(Transform transform)
		{
			PathMove pathMove = transform.GetComponent<PathMove>();
			if (pathMove == null)
			{
				pathMove = transform.gameObject.AddComponent<PathMove>();
			}
			pathMove._transform = transform;
			return pathMove;
		}

		public static void Stop(Transform transform)
		{
			PathMove component = transform.GetComponent<PathMove>();
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

		public void Follow(float moveSpeed, FollowType followType, FollowDirection followDirection)
		{
			this.moveSpeed = moveSpeed;
			this.followType = followType;
			this.followDirection = followDirection;
			this.StopFollowing();
			int closestLineIndex = this.GetClosestLineIndex(this._transform.position);
			this._currentIndex = this.GetClosestPointIndex(closestLineIndex * 20, this._transform.position);
			base.StartCoroutine("FollowPath");
		}

		public void FollowToPoint(float moveSpeed, Vector2 targetPos)
		{
			this.moveSpeed = moveSpeed;
			this.followType = FollowType.Once;
			this.StopFollowing();
			int closestLineIndex = this.GetClosestLineIndex(this._transform.position);
			this._currentIndex = this.GetClosestPointIndex(closestLineIndex * 20, this._transform.position);
			closestLineIndex = this.GetClosestLineIndex(targetPos);
			int closestPointIndex = this.GetClosestPointIndex(closestLineIndex * 20, targetPos);
			this.followDirection = ((this._currentIndex >= closestPointIndex) ? FollowDirection.Backward : FollowDirection.Forward);
			base.StartCoroutine("FollowPathToPoint", closestPointIndex);
		}

		public void StopFollowing()
		{
			base.StopCoroutine("FollowPath");
			base.StopCoroutine("FollowPathToPoint");
		}

		private IEnumerator FollowPath()
		{
			PathMove._FollowPath_c__Iterator0 _FollowPath_c__Iterator = new PathMove._FollowPath_c__Iterator0();
			_FollowPath_c__Iterator._this = this;
			return _FollowPath_c__Iterator;
		}

		private IEnumerator FollowPathToPoint(int targetIndex)
		{
			PathMove._FollowPathToPoint_c__Iterator1 _FollowPathToPoint_c__Iterator = new PathMove._FollowPathToPoint_c__Iterator1();
			_FollowPathToPoint_c__Iterator.targetIndex = targetIndex;
			_FollowPathToPoint_c__Iterator._this = this;
			return _FollowPathToPoint_c__Iterator;
		}

		private void MoveTo(int pointIndex)
		{
			Vector3 vector = this.linePoints[pointIndex];
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
			return (this._transform.position - this.linePoints[pointIndex]).sqrMagnitude < 0.1f;
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
			return this.linePoints.Count - 1;
		}

		private int EndIndex()
		{
			if (this.followDirection == FollowDirection.Backward)
			{
				return 0;
			}
			return this.linePoints.Count - 1;
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
			List<Vector3> list = this.points;
			Vector3 a = this.ComputeClosestPointFromPointToLine(pos, list[0], list[1]);
			float num = (a - pos).sqrMagnitude;
			int result = 0;
			for (int i = 1; i < list.Count - 1; i++)
			{
				Vector3 a2 = this.ComputeClosestPointFromPointToLine(pos, list[i], list[i + 1]);
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
			int num = Mathf.Min(nStartIndex + 20, this.linePoints.Count - 1);
			Vector3 a = this.linePoints[nStartIndex];
			float num2 = (a - pos).sqrMagnitude;
			int result = nStartIndex;
			for (int i = nStartIndex + 1; i <= num; i++)
			{
				Vector3 a2 = this.linePoints[i];
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

		private void SetStraightLine()
		{
			List<Vector3> list = this.points;
			if (list.Count < 2)
			{
				return;
			}
			for (int i = 0; i < list.Count - 1; i++)
			{
				for (float num = 0f; num <= 1f; num += 0.05f)
				{
					Vector3 item = list[i] * (1f - num) + list[i + 1] * num;
					this.linePoints.Add(item);
				}
			}
			this.linePoints.Add(list[list.Count - 1]);
		}
	}
}
