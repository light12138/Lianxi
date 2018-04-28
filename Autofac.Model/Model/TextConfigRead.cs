using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Model
{
    public class TextConfigRead : IConfigRead
    {
        public Guid? Id { get; set; }
        public string Read()
        {
            Id = Id == null ? Guid.NewGuid() : Id;
            return "这是TextConfigRead在读取: guid="+ Id;
        }
    }
}
