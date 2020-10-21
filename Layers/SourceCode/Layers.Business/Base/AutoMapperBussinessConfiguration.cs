using AutoMapper;
using Layers.Business.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Business.Base
{
    public static class AutoMapperBussinessConfiguration
    {
        #region Members

        private static bool isInitialized = false;
        private static object locker = new object();

        #endregion

        #region Method

        public static void Configure(params Profile[] externalProfiles)
        {
            if (!isInitialized)
            {
                lock (locker)
                {
                    if (!isInitialized)
                    {
                        Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfile(UserMapper.Instance);

                        if (externalProfiles != null)
                        {
                            foreach (var profile in externalProfiles)
                            {
                                cfg.AddProfile(profile);
                            }
                        }
                    });
                        isInitialized = true;
                    }
                }
            }
        }

        #endregion
    }
}
