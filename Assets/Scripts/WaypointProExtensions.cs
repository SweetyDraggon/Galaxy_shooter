using Mr1;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class WaypointProExtensions
{
	private sealed class _Duration_c__AnonStorey0
	{
		internal PathFollower pathFollower;

		internal void __m__0()
		{
			this.pathFollower.StopFollowing();
		}
	}

	public static PathFollower FollowPath(this Transform transform, string pathName, float moveSpeed, FollowType followType = FollowType.Once, FollowDirection followDirection = FollowDirection.Forward)
	{
		PathData pathData = WaypointManager.instance.GetPathData(pathName);
		PathFollower pathFollower = PathFollower.Create(transform);
		if (pathData != null)
		{
			pathFollower.Follow(pathData, moveSpeed, followType, followDirection);
		}
		else
		{
			UnityEngine.Debug.LogError(string.Format("[WaypointManager] couldn't find path('{0}')", pathName));
		}
		return pathFollower;
	}

	public static PathFollower FollowPathToPoint(this Transform transform, string pathName, Vector2 targetPos, float moveSpeed)
	{
		PathData pathData = WaypointManager.instance.GetPathData(pathName);
		PathFollower pathFollower = PathFollower.Create(transform);
		if (pathData != null)
		{
			pathFollower.FollowToPoint(pathData, moveSpeed, targetPos);
		}
		else
		{
			UnityEngine.Debug.LogError(string.Format("[WaypointManager] couldn't find path('{0}')", pathName));
		}
		return pathFollower;
	}

	public static void StopFollowing(this Transform transform)
	{
		PathFollower.Stop(transform);
	}

	public static PathFollower Duration(this PathFollower pathFollower, float duration)
	{
		Cooltimer.Set(pathFollower, duration, delegate
		{
			pathFollower.StopFollowing();
		});
		return pathFollower;
	}

	public static PathFollower Flip(this PathFollower pathFollower, bool useFlip)
	{
		pathFollower.SetFlip(useFlip);
		return pathFollower;
	}

	public static PathFollower LookForward(this PathFollower pathFollower, bool useLookForward)
	{
		pathFollower.SetLookForward(useLookForward);
		return pathFollower;
	}

	public static PathFollower SmoothLookForward(this PathFollower pathFollower, bool useSmoothLookForward, float rotateSpeed)
	{
		pathFollower.SetSmoothLookForward(useSmoothLookForward, rotateSpeed);
		return pathFollower;
	}

	public static PathFollower Log(this PathFollower pathFollower, bool logMessage)
	{
		pathFollower.logMessage = logMessage;
		return pathFollower;
	}
}
