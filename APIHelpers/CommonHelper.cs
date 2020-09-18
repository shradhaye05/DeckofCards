using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace DeckofcardsApi.APIHelpers
{
    class CommonHelper
    {
        /// <summary>
        /// Serializing the test data into string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testData"></param>
        /// <returns></returns>
        public static string SerializeData<T>(T testData)
        {
            return JsonConvert.SerializeObject(testData);
        }

        /// <summary>
        /// Deserializing the Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T TryDeserializeResponse<T>(string result)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(result as string);
            }
            catch (JsonReaderException)
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Deserializing the Response with out variable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(string result, out T resultValue)
        {
            try
            {
                var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                var str = JsonConvert.SerializeObject(result, settings);
                resultValue = JsonConvert.DeserializeObject<T>(result);
                return true;
            }
            catch
            {
                resultValue = default(T);
                return false;
            }
        }

    }

    public class ResponseModel<T>
    {
        public T Data { get; set; }

        public string ActualResponse { get; set; }

        public HttpResponseMessage Response { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class NullToEmptyStringResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        return type.GetProperties()
                .Select(p => {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    return jp;
                }).ToList();
    }
}
    public class NullToEmptyStringValueProvider : Newtonsoft.Json.Serialization.IValueProvider
    {
        PropertyInfo _MemberInfo;
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
            return result;

        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    }
}

