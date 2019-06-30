using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.WebApiClient.Request
{
    /// <summary>
    /// Defines an abstract base class for a HTTP header value.
    /// </summary>
    public abstract class HttpHeaderValue
    {
        /// <summary>
        /// Return the header's value as a string.
        /// </summary>
        public abstract string StringValue { get; }
    }

    /// <summary>
    /// Defines a class a HTTP header value. 
    /// </summary>
    /// <typeparam name="T">The type of the header value.</typeparam>
    public class HttpHeaderValue<T> : HttpHeaderValue
    {
        /// <summary>
        /// The value of the header. 
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Returns the string representation of the header value. 
        /// </summary>
        public override string StringValue
        {
            get { return Value.ToString(); }
        }

        /// <summary>
        /// Default constructor of the HttpHeaderValue class.
        /// </summary>
        /// <param name="value">The typed value of the header.</param>
        public HttpHeaderValue(T value)
        {
            Value = value;
        }
    }
}
