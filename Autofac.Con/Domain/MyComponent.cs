using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Model.Interface;

namespace Autofac.Con.Domain
{

    
    public class MyComponent
    {
        public MyComponent() { }

        public MyComponent(ILogger logger) {
            _logger = logger;
        }

        public MyComponent(ILogger logger,IConfigRead read) {
            _read = read;
            _logger = logger;
        }


        private ILogger _logger;

        private IConfigRead _read;


        public string Read()
        {
            return _read.Read();
        }

        public string Write()
        {
            return _logger.Write();
        }

        public DateTime Date { get; set; }
    }
}
