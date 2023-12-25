using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkinDataSO")]
public class SkinDataSO : ScriptableObject
{
    [SerializeField] private List<SkinData> _skinList = new List<SkinData>();
    public List<Texture2D> GetItemImages()
    {
        List<Texture2D> temp = new List<Texture2D>();
        for (int i = 0; i < _skinList.Count; i++)
        {
            temp.Add(_skinList[i].image);
        }
        return temp;
    }
    public SkinData GetItemDataById(int id)
    {
        return _skinList[id];
    }
    public int GetItemCount()
    {
        return _skinList.Count;
    }
}
