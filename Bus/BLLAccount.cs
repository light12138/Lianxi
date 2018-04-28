using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class BLLAccount
    {
        public BLLAccount(ILogger logger,IConfigRead configRead)
        {
            _logger = logger;
            _configRead = configRead;
        }

        private ILogger _logger;

        private IConfigRead _configRead { get; set; }


        public string Write()
        {
          return  _logger.Write();
        }

        public string Read()
        {
            return _configRead.Read();
        }
    }
}
