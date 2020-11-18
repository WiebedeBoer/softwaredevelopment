using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public interface IListener
    {
        public void WaitSequence();
        public void StartListening();
        public void Connect();
    }
}
