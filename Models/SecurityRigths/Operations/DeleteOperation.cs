﻿using System;

namespace Models.SecurityRigths.Operations
{
    public class DeleteOperation : IRight
    {
        public string Id
        {
            get
            {
                return "Delete";
            }
        }

        public string Name
        {
            get
            {
                return "Удаление";
            }
        }
    }
}
