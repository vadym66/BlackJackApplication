﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.ViewModels
{
    public class HistoryServiceViewModel
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public List<CardServiceViewModel> Cards { get; set; }
    }
}
