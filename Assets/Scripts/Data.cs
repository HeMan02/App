using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static Profile[] datingProfile;
    public static List<Profile> chats = new List<Profile>();

    public static Profile SearchProfile(Sprite profilePic)
    {
        for (int i = 0; i < datingProfile.Length; i++)
        {
            if (datingProfile[i].profilePic == profilePic)
                return datingProfile[i];
        }
        return null;
    }
}
