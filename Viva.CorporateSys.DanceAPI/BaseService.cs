using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Viva.CorporateSys.DanceAPI
{
    public class BaseService
    {
        public static IKernel Kernel =null;

        static BaseService()
        {
             Kernel = new StandardKernel(new APIModule());
        }
    }
}
