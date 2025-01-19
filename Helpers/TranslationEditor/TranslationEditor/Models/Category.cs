using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationEditor.Models
{
    public class Category
    {
        public string Name { get; set; }
        public Dictionary<string,string> Texts { get; set; }
    }
}
