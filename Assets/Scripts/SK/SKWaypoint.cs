using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SK
{
	public static class SKWaypoint
	{
		private sealed class _Duration_c__AnonStorey0
		{
			internal PathMove pathFollower;

			internal void __m__0()
			{
				this.pathFollower.StopFollowing();
			}
		}

		public static PathMove FollowPath(this Transform transform, string pathName, float moveSpeed, FollowType followType = FollowType.Once, FollowDirection followDirection = FollowDirection.Forward)
		{
			PathMove pathMove = PathMove.Create(transform);
			pathMove.Follow(moveSpeed, followType, followDirection);
			return pathMove;
		}

		public static PathMove FollowPathToPoint(this Transform transform, string pathName, Vector2 targetPos, float moveSpeed)
		{
			PathMove pathMove = PathMove.Create(transform);
			pathMove.FollowToPoint(moveSpeed, targetPos);
			return pathMove;
		}

		public static void StopFollowing(this Transform transform)
		{
			PathMove.Stop(transform);
		}

		public static PathMove Duration(this PathMove pathFollower, float duration)
		{
			PathMove.Set(duration, delegate
			{
				pathFollower.StopFollowing();
			});
			return pathFollower;
		}

		public static PathMove Flip(this PathMove pathFollower, bool useFlip)
		{
			pathFollower.SetFlip(useFlip);
			return pathFollower;
		}

		public static PathMove LookForward(this PathMove pathFollower, bool useLookForward)
		{
			pathFollower.SetLookForward(useLookForward);
			return pathFollower;
		}

		public static PathMove SmoothLookForward(this PathMove pathFollower, bool useSmoothLookForward, float rotateSpeed)
		{
			pathFollower.SetSmoothLookForward(useSmoothLookForward, rotateSpeed);
			return pathFollower;
		}
	}
}
