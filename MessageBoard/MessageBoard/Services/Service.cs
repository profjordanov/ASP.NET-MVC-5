using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageBoard.Data;

namespace MessageBoard.Services
{
    public class Service
    {
        protected MessageBoardContext Context { get; }
        public Service()
        {
            this.Context = new MessageBoardContext();
        }
    }
}