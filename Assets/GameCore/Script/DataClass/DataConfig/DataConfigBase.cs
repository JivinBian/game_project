using System;
using System.Collections;
using System.Reflection;

namespace GameCore.Script.DataClass.DataConfig
{
	public class DataConfigBase
	{
        public int Id { get; protected set; }
		protected JsonData _jsonData;
		public void Parse(JsonData pJsonData)
		{
			_jsonData = pJsonData;
			if (_jsonData != null)
			{
				Map();
                SetID();
			}
		}

	    protected virtual void SetID()
	    {
	        if (_jsonData != null)
	        {
	            IDictionary tDictionaryData = (IDictionary) _jsonData;

                if (tDictionaryData.Contains("id"))
	            {
                    Id=int.Parse(_jsonData["id"].ToString());
	            }
                else if (tDictionaryData.Contains("Id"))
                {
                    Id = int.Parse(_jsonData["Id"].ToString());
                }
                else if (tDictionaryData.Contains("ID"))
                {
                    Id = int.Parse(_jsonData["ID"].ToString());
                }
                else
                {
                    throw new Exception("Config must contain segment \'id\':");
                }
            }
	    }
		protected virtual void Map()
		{
			Type tType=GetType();
			PropertyInfo[] tPropertyInfos = tType.GetProperties();
			object tObject;
			for (int i = 0; i < tPropertyInfos.Length; i++)
			{
				PropertyInfo tPropertyInfo = tPropertyInfos[i];
				if (((IDictionary)_jsonData).Contains(tPropertyInfo.Name))
				{
					object tJsonValue = _jsonData[tPropertyInfo.Name];
					tObject = string.IsNullOrEmpty(tJsonValue.ToString()) ? null : Convert.ChangeType(tJsonValue.ToString(), tPropertyInfo.PropertyType);
					tPropertyInfo.SetValue(this,tObject,null);
				}
			}
		}
	}
}

