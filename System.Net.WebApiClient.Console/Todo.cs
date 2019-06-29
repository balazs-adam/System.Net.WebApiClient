using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.WebApiClient.Console
{
    public class Todo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
