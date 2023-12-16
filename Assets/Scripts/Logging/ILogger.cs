using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public interface ILogger
    {
        public void Info(object message);
        public void Warn(object message);
        public void Error(object message);

        public void CreateLog();
    }
}
