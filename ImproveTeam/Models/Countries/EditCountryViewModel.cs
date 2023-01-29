﻿using System.ComponentModel;

namespace ImproveTeam.Models.Countries
{
    public class EditCountryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название:")]
        public string Name { get; set; }

        [DisplayName("Код страны:")]
        public string Code { get; set; }
    }
}
