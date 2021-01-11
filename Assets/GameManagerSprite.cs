using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSprite : Singleton<GameManagerSprite>
{


    public Sprite[] ShapeList;

    public Color[] ColorList;

    public RectTransform TimingBar;
    public SpriteShape SpriteShape;
    public TextMeshProUGUI Result;


    public float[] StartIntervals;

    public Image TargetColorImage;
    public Image TargetShapeImage;

    public AudioClip WinSound;
    public AudioClip LoseSound;


    private AudioSource audioSource;

    private int clickedTime = 0;

    private float currentTime;

    private int targetShapeId;

    private int targetColorID;

    private int currentShapeID;

    private int currentColorID;

    private bool gameEnded;

    protected void Awake()
    {
        this.clickedTime = 0;
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public float RemainTimeRate
    {
        get
        {
            var totalTime = 0f;
           
            if (this.StartIntervals.Length > this.clickedTime)
                totalTime = this.StartIntervals[this.clickedTime];
            else
                totalTime = this.StartIntervals[this.StartIntervals.Length - 1];

            return this.currentTime / totalTime;
        }
    }

    protected void Start()
    {
        this.currentTime = this.StartIntervals[0];

        var id = Random.Range(0, this.ColorList.Length);
        this.TargetColorImage.color = this.ColorList[id];
        this.targetColorID = id;
        id = Random.Range(0, this.ShapeList.Length);
        this.TargetShapeImage.sprite = this.ShapeList[id];
        this.targetShapeId = id;

        this.StartCoroutine(this.UpdateTimingBar());
    }

    private void WinGame()
    {
        this.StopAllCoroutines();
        this.gameEnded = true;
        this.Result.text = "YOU WIN!";
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.WinSound);
    }

    private void LoseGame()
    {
        this.StopAllCoroutines();
        this.gameEnded = true;
        this.Result.text = "YOU LOSE!";
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.LoseSound);
    }

 
    public void OnShapeClicked()
    {
        if (this.gameEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (this.currentColorID == this.targetColorID && this.currentShapeID == this.targetShapeId)
        {
            this.LoseGame();
        }else
        {

            this.SpriteShape.SwitchRandomColor();
            this.SpriteShape.SwitchRandomShape();
            this.ResetTimer();
        }
    }

    public Color GetRandomColor()
    {
        var id = Random.Range(0, this.ColorList.Length);
        if(id == this.currentColorID)
        {
            id += 1;
            id %= this.ColorList.Length;
        }
        this.currentColorID = id;
        return this.ColorList[id];
    }

    public Sprite GetRandomShape()
    {
        var id = Random.Range(0, this.ShapeList.Length);
        if (id == this.currentShapeID)
        {
            id += 1;
            id %= this.ShapeList.Length;
        }
        this.currentShapeID = id;
        return this.ShapeList[id];
    }

    public void ResetTimer()
    {
        this.clickedTime++;
        if (this.StartIntervals.Length > this.clickedTime)
            this.currentTime = this.StartIntervals[this.clickedTime];
        else
            this.currentTime = this.StartIntervals[this.StartIntervals.Length - 1];
    }

    IEnumerator UpdateTimingBar()
    {
        while(currentTime>0)
        {
            currentTime -= Time.deltaTime;
            yield return null;

        }
        this.TimeUp();
    }

    private void TimeUp()
    {
        if(this.currentColorID == this.targetColorID && this.currentShapeID == this.targetShapeId)
        {

            this.WinGame();
        }else
        {
            this.LoseGame();
        }
    }
}
