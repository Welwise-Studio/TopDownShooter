using System;

namespace Logging
{
    public interface ILogable
    {
        public Message[] GetTrace();
        public event Action<Message> OnPrint;
    }
}
