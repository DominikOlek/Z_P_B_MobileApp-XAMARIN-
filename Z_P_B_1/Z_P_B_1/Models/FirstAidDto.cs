using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models
{
    public class FirstAidDto
    {
        public int Id { get; set; }
        public bool IsOther { get; set; }
        public string Title { get; set; }
        public string Steps { get; set; }
        public string OtherInforamtion { get; set; }
        public string TitlePhotoName { get; set; }
        public string OtherPhotoName { get; set; }
    }
}
