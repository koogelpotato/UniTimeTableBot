using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTimeTableBot
{
    public abstract class AbstractQueryHandler : IHandler
    {
        private IHandler? _nextHandler;
        public string Handle(string callBackQuery)
        {
            if(this._nextHandler != null)
            {
                return this._nextHandler.Handle(callBackQuery);
            }
            else
            {
                return null;
            }
        }

        public virtual IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
    }
}
