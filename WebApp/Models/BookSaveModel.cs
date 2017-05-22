using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BookSaveModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string  ContentDescription { get; set; }
        public EnumBookSave EnumSaveBook { get; set; }
    }
}