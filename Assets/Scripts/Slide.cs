using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
	public Sprite[] sprites;

	public Transform trnList;

	public Transform trnDot;

	public Vector3 pos;

	public float dis;

	private int count;

	private int index;

	public Sprite[] spriteDots;

	private void Start()
	{
		this.pos = this.trnList.transform.position;
		this.count = this.sprites.Length;
		this.index = 0;
		this.Move();
		for (int i = 0; i < this.trnDot.childCount; i++)
		{
			this.trnDot.GetChild(i).GetComponent<Image>().sprite = this.spriteDots[(i != this.index) ? 0 : 1];
		}
	}

	public void OnClickRate()
	{
	}

	public void OnClickUpdate()
	{
	}

	public void OnClickStore()
	{
	}

	private void Move()
	{
		this.trnList.DOMove(this.pos - new Vector3(this.dis * (float)(this.index + 1), 0f), 1f, false).SetDelay(1f).OnComplete(delegate
		{
			this.index++;
			if (this.index >= this.count)
			{
				this.index = 0;
				this.trnList.position = this.pos;
			}
			for (int i = 0; i < this.trnDot.childCount; i++)
			{
				this.trnDot.GetChild(i).GetComponent<Image>().sprite = this.spriteDots[(i != this.index) ? 0 : 1];
			}
			this.Move();
		});
	}
}
