using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFID.Command
{
    public class TagPool
    {
        private Dictionary<String, TagStruct> dictTagStruct;
        public delegate void ActionEven(TagStruct oTagStruct);
        private ActionEven removeEven, addEven, modifyEven;
        public TagPool()
        {
            dictTagStruct = new Dictionary<String, TagStruct>();
        }

        public void RegisterAddEven(ActionEven action)
        {
            if (action != null)
                addEven = action;
        }

        public void RegisterRemoveEven(ActionEven action)
        {
            if (action != null)
                removeEven = action;
        }

        public void RegisterModifyEven(ActionEven action)
        {
            if (action != null)
                modifyEven = action;
        }

        public void AddTag(TagStruct oTagStruct)
        {
            if (oTagStruct == null)
                throw new Exception("oTagStruct is null");
            if (String.IsNullOrEmpty(oTagStruct.TagID.Trim()))
                throw new Exception("oTagStruct TagId is null");
            oTagStruct.TagID = oTagStruct.TagID.Replace(" ", "");
            try
            {
                lock (dictTagStruct)
                {
                    if (dictTagStruct.ContainsKey(oTagStruct.TagID))
                    {
                        if (String.IsNullOrEmpty(oTagStruct.DiscoveryTime))
                            oTagStruct.DiscoveryTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        dictTagStruct[oTagStruct.TagID] = oTagStruct;
                        if (modifyEven != null)
                            modifyEven(oTagStruct);
                    }
                    else
                    {
                        dictTagStruct.Add(oTagStruct.TagID, oTagStruct);
                        if (addEven != null)
                            addEven(oTagStruct);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void RemoveTag(String tagId)
        {
            if (String.IsNullOrEmpty(tagId.Trim()))
                throw new Exception("TagId is null");
            try
            {
                lock (dictTagStruct)
                {
                    if (dictTagStruct.ContainsKey(tagId))
                    {
                        dictTagStruct.Remove(tagId);
                        if (removeEven != null)
                            removeEven(dictTagStruct[tagId]);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public String SearchPoolByTagId(String tagId)
        {
            lock (dictTagStruct)
            {
                if (dictTagStruct.ContainsKey(tagId))
                {
                    return dictTagStruct[tagId].TagID;
                }
            }
            return null;
        }

        public void ClearPool()
        {
            lock (dictTagStruct)
            {
                dictTagStruct.Clear();
            }
        }
    }
}
