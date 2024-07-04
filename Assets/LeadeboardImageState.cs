using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LeadeboardImageState : MonoBehaviour
{
    [SerializeField] private ImageZoom[] imageZooms;
    [SerializeField] private OnPaintingClickListener onPaintingClickListener;
    // Start is called before the first frame update

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector3(2f, 2f, 2f) , .5f).SetEase(Ease.OutBack);
        onPaintingClickListener.Response.AddListener(OnPaintClick);
    }

    private void OnPaintClick(Sprite arg0, bool isZoom)
    {
        foreach (var img in imageZooms)
        {
            if (!isZoom)
            {
                StartCoroutine(delayReactivation(img));
            }
            else
            {
                img.SetIsZoomOn(isZoom);
            }
                
        }
    }

    IEnumerator delayReactivation(ImageZoom img)
    {
        yield return new WaitForSeconds(.5f);
        img.SetIsZoomOn(false);
    }
    
    private void OnDisable()
    {
        onPaintingClickListener.Response.RemoveListener(OnPaintClick);
    }
}
