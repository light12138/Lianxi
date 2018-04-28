using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public class SignatureVerificationException:Exception
    {
        private const string ExpectedKey = "Expected";
        private const string ReceivedKey = "Received";

        public SignatureVerificationException(string message)
          : base(message)
        {
        }

        /// <summary>
        /// Expected key.
        /// </summary>
        public string Expected
        {
            get => GetOrDefault<string>(ExpectedKey);
            internal set => this.Data.Add(ExpectedKey, value);
        }

        /// <summary>
        /// Received key.
        /// </summary>
        public string Received
        {
            get => GetOrDefault<string>(ReceivedKey);
            internal set => this.Data.Add(ReceivedKey, value);
        }

        /// <summary>
        /// Retrieves the value for the provided key, or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected T GetOrDefault<T>(string key) => this.Data.Contains(key) ? (T)this.Data[key] : default(T);
    }
}
