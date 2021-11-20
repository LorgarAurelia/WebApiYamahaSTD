using Crypto.Library;
using MiddlewareExceptionPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiyamaha.Services.YamahaBll.Models;

namespace WebApiyamaha.Services.YamahaBll.Extensions
{
    public static class ParamExtension
    {
        public static string ToIdx(this Parameter parameter)
        {
            var strParam = JsonConvert.SerializeObject(parameter);
            return Crypt.Zip(strParam);
        }

        public static Parameter ToParameter(this string idx)
        {
            Parameter param = new Parameter();

            try
            {
                var strParam = Crypt.Unzip(idx);
                param = JsonConvert.DeserializeObject<Parameter>(strParam);
            }
            catch
            {
                throw new ServiceException("Некорректный параметр «Idx»", 400);
            }

            return param;
        }
    }
}
