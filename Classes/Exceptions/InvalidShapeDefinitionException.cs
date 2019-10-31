﻿using System;

namespace csharp_08
{
    public class InvalidShapeDefinitionException : Exception
    {
        public InvalidShapeDefinitionException(string Shape, string Comment = "") : base(String.Format("Invalid Shape Definition for Shape: {0}\nComment:\n{1}", Shape, Comment))
        {
        }
    }
}