using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Data.Save
{
    [CreateAssetMenu(fileName = "DSGraphSaveData", menuName = "Dialogue System/Save Data/Graph Save Data")]
    public class DSGraphSaveDataSO : ScriptableObject
    {
       [field: SerializeField] public string FileName { get; set; }
       [field: SerializeField] public List<DSGroupSaveData> Groups { get; set; }
       [field: SerializeField] public List<DSNodeSaveData> Nodes { get; set; }
       [field: SerializeField] public List<string> OldGroupNames { get; set; }
       [field: SerializeField] public List<string> OldUngroupedNodeNames { get; set; }
       [field: SerializeField] public SerializableDictionary<string, List<string>> OldGroupedNodeNames { get; set; }

       public void Initialize(string fileName)
       {
           FileName = fileName;

           Groups = new List<DSGroupSaveData>();
           Nodes = new List<DSNodeSaveData>();
       }
    }
}
