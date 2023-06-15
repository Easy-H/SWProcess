using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : Singleton<FirebaseManager>
{
    public LevelData GetLevelData(string value) {
        LevelData retval = null;

        // retval을 firebase와통신하여 얻어와야 함

        return retval;
    }

    public void UploadLevel(LevelData data) { 
        // firebase와 통신하여 현재 레벨을 업로드 함
    }
}
