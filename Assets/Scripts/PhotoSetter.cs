using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSetter : MonoBehaviour {

    [SerializeField]
    Text name;
    [SerializeField]
    Image profilePic;
    [SerializeField]
    Image pic;
    [SerializeField]
    Text like;
    [SerializeField]
    Text comment;
    [SerializeField]
    Image likeButton;

    bool liked;
    long likeCount;

    public void SetPhoto(string name,Sprite profilePic,Sprite pic,long like,string comment)
    {
        this.name.text = name;
        this.profilePic.sprite = profilePic;
        this.pic.sprite = pic;
        this.like.text = like.ToString() + " liked this";
        this.comment.text = comment;

        this.likeCount = like;
    }

    public void PressLike()
    {
        liked = !liked;
        this.like.text = liked ? (likeCount+1).ToString() + " liked this" : likeCount.ToString()+" liked this";
        likeButton.color = liked ? Color.red : Color.gray;
    }

}
