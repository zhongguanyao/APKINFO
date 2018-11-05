using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APKINFO.Interface
{
    public interface ILog
    {
        void Log(string msg);
        void BlankLine();
    }
}
