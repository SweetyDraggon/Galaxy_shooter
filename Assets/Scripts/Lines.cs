using DG.Tweening;
using Mr1;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lines : MonoBehaviour
{
	public GameObject go;

	public Sprite[] spriteLines;

	public List<GameObject> linesActive = new List<GameObject>();

	public List<GameObject> linesDisable = new List<GameObject>();

	public SelectLevelManager selectLevel;

	private float t = -1f;

	public int max;

	public int id;

	public ScrollRect scroll;

	private bool isDone;

	public PathData pathData;

	private void Start()
	{
		this.scroll.enabled = false;
		this.CreateLines();
	}

	private void CreateLines()
	{
		this.isDone = false;
		this.max = Mathf.Min(Mathf.Max(PlayerPrefs.GetInt("max", 1)-1, 0), this.selectLevel.btnLevels.Length - 1);
		
		Vector3 position = this.selectLevel.btnLevels[this.max].transform.position;
		for (int i = 0; i < this.pathData.linePoints.Count - 1; i += 4)
		{
			Vector3 vector = this.pathData.linePoints[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go);
			gameObject.name = "line " + i;
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.position = vector;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.eulerAngles = this.GetAngle(gameObject.transform.position, this.pathData.linePoints[i + 1]);
			if (!this.isDone)
			{
				if (Vector2.Distance(vector, position) < 0.5f)
				{
					this.isDone = true;
				}
				this.linesActive.Add(gameObject);
			}
			else
			{
				this.linesDisable.Add(gameObject);
			}
			gameObject.GetComponent<Image>().sprite = (this.isDone ? this.spriteLines[1] : this.spriteLines[0]);

			if (i == 1516 || i == 1520 || i == 1524 || i == 1528 || i == 1532)
				gameObject.SetActive(false);
		}
		this.RunUpdate();
	}

	public void CheckLines()
	{
		//MonoBehaviour.print(PlayerPrefs.GetInt("max") + "/" + this.max);
		if (this.max == PlayerPrefs.GetInt("max", 1) || this.linesDisable == null)
		{
			return;
		}
		this.isDone = false;
		this.max = PlayerPrefs.GetInt("max");
		if (this.max >= this.selectLevel.btnLevels.Length)
		{
			this.max = this.selectLevel.btnLevels.Length;// - 1;
			PlayerPrefs.SetInt("max", this.max);
		}
		for (int i = 0; i < this.max; i++)
		{
			this.selectLevel.btnLevels[i].interactable = true;
		}
		Vector3 position = this.selectLevel.btnLevels[Mathf.Min(PlayerPrefs.GetInt("max", 1)-1, this.selectLevel.btnLevels.Length - 1)].transform.position;
		for (int j = 0; j < this.linesDisable.Count - 1; j++)
		{
			this.linesDisable[j].GetComponent<Image>().sprite = this.spriteLines[0];
			this.linesDisable[j].transform.DOLocalMove(this.linesDisable[j + 1].transform.localPosition, 0.8f, false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
			if (Vector2.Distance(this.linesDisable[j].transform.position, position) < 0.5f)
			{
				this.isDone = true;
				return;
			}
		}
	}

	public Vector3 GetAngle(Vector2 start, Vector2 end)
	{
		return new Vector3(0f, 0f, Mathf.Atan2(end.y - start.y, end.x - start.x) * 57.29578f - 90f);
	}

	private void RunUpdate()
	{
		for (int i = 0; i < this.linesActive.Count - 1; i++)
		{
			this.linesActive[i].transform.DOLocalMove(this.linesActive[i + 1].transform.localPosition, 0.8f, false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
		}
		float num = this.selectLevel.btnLevels[PlayerPrefs.GetInt("map")].transform.position[1] - this.scroll.transform.GetChild(0).position[1];
		this.scroll.enabled = true;
		this.linesActive.Clear();
	}
}
