using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterObservable
{
    public class MessageSubject : Subject
    {
        private MessageModel message = new();

        public MessageModel Message
        {
            get => message;
            set
            {
                message = value;
                Notify();
            }
        }
    }
}
