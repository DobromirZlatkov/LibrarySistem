namespace DigitalLibrary.Web.ViewModels.Common
{
    ﻿using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SubmitSearchModel
    {
        public string MatchSearch { get; set; }

        public string GenreSearch { get; set; }

        public int YearSearch { get; set; }
    }
}
