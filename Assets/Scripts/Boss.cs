using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
	public SkeletonAnimation skeAni;

	public UnityAction<string, string> CallbackEvent;

	public UnityAction<string> CallbackStart;

	public UnityAction<string> CallbackComplete;

	public virtual void Start()
	{
		this.skeAni.AnimationState.Event += new Spine.AnimationState.TrackEntryEventDelegate(this.State_Event);
		this.skeAni.AnimationState.Start += new Spine.AnimationState.TrackEntryDelegate(this.AnimationState_Start);
		this.skeAni.AnimationState.Complete += new Spine.AnimationState.TrackEntryDelegate(this.AnimationState_Complete);
	}

	private void AnimationState_Complete(TrackEntry trackEntry)
	{
		if (this.CallbackComplete != null)
		{
			this.CallbackComplete(trackEntry.animation.name);
		}
	}

	private void AnimationState_Start(TrackEntry trackEntry)
	{
		if (this.CallbackStart != null)
		{
			this.CallbackStart(trackEntry.animation.name);
		}
	}

	private void State_Event(TrackEntry trackEntry, Spine.Event e)
	{
		if (this.CallbackEvent != null)
		{
			this.CallbackEvent(trackEntry.animation.name, e.data.name);
		}
	}
}
