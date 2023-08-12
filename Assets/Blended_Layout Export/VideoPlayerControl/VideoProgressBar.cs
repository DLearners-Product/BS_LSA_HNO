using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField] private GameObject videoLoading, spawnedLoader;
    public Texture2D thumbnailTexture;
    public Camera Cam;
    public Image progress;
    double lastTimePlayed;
    float waitTime = 3f,
            time = 0f;
    float skipFrame;
    bool requestSkip,
        requestPrevSkip,
        requestNextSkip,
        reachedEnd = false;

    private void Awake()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        videoPlayer.targetTexture.Release();
        videoPlayer.loopPointReached += VideoFinished;
    }

    void VideoFinished(VideoPlayer vp){
        reachedEnd = true;
        Destroy(spawnedLoader);
    }

    private void OnEnable()
    {
        if (thumbnailTexture != null)
            Graphics.Blit(thumbnailTexture, videoPlayer.targetTexture);
        // StartCoroutine(GetThumbnail());
    }

    IEnumerator GetThumbnail()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForEndOfFrame();
        }

        videoPlayer.frame = 10;
        videoPlayer.Play();
        videoPlayer.Pause();
    }

    private void Update()
    {
        Check_Video_Buffering();
        if (videoPlayer.frameCount > 0)
            progress.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
    }

    private void Check_Video_Buffering()
    {
        if (videoPlayer.isPlaying && (Time.frameCount % (int)(videoPlayer.frameRate + 1)) == 0)
        {
            if (lastTimePlayed == videoPlayer.time)
            {
                // Debug.Log($"buffering");
                time += Time.deltaTime;
                if (spawnedLoader == null && time > waitTime){
                    spawnedLoader = Instantiate(videoLoading, gameObject.transform);
                }
            }
            else
            {
                // Debug.Log($"not buffering");
                time = 0f;
                if (spawnedLoader != null)
                    Destroy(spawnedLoader);
            }
            lastTimePlayed = videoPlayer.time;
        }
        else if (!Main_Blended.OBJ_main_blended.B_pause && !videoPlayer.isPlaying && !reachedEnd)
        {
            if (spawnedLoader == null){
                spawnedLoader = Instantiate(videoLoading, gameObject.transform);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        if(reachedEnd){
            videoPlayer.Play();
            reachedEnd = false;
        }
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, Cam, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    private void SkipToPercent(float pct)
    {
        skipFrame = videoPlayer.frameCount * pct;
        videoPlayer.frame = (long)skipFrame;
        if (spawnedLoader == null && !Main_Blended.OBJ_main_blended.B_pause){
            spawnedLoader = Instantiate(videoLoading, gameObject.transform);
        }
    }
}