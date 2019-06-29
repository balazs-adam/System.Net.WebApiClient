using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.WebApiClient.Console
{
    public class Photo
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
