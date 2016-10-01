﻿using Aimp.Entities;
using System;

namespace Aimp.Model.Entities
{
    public class User : Entity, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstNameGenitive { get; set; }
        public string LastNameGenitive { get; set; }
        public string MiddleNameGenitive { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
