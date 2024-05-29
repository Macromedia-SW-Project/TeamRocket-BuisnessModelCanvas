using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Course.PrototypeScripting
{
    [System.Serializable]
    public class GenericVariable
    {
        public string name;
        public int value;
    }

    public class VariableData : ScriptableObject
    {

        public List<GenericVariable> variableInfos;

        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            foreach(GenericVariable genVar in variableInfos)
            {
                names.Add(genVar.name);
            }
            return names;
        }
    }
}