using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace LJJSCAD.Util
{
    class HashUtil
    {
        public static Hashtable MoveHashTableZeroValue(Hashtable ht)
        {
            Hashtable returnht = new Hashtable();
            foreach (DictionaryEntry de in ht)
            {
                double xvalue = (double)de.Value;
                if (Math.Abs(xvalue) > 0.0001)
                    returnht.Add(de.Key, de.Value);
            }
            return returnht;
        }
        public static ArrayList GetHastablePaiXuList(Hashtable ht)
        {
            ArrayList al = new ArrayList(ht.Keys);
            al.Sort();
            return al;
        }
        public static object FindObjByKey(string keyValue,Hashtable hashTable)
        {
            Object obj = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                string tmpValue = keyValue.Trim();
                if (null != hashTable && hashTable.Count > 0)
                {
                    if (hashTable.ContainsKey(tmpValue))
                        obj = hashTable[tmpValue];
                }
            }
            return obj;
        }
    }
}
