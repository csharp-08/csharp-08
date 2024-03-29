﻿using System;

namespace csharp_08
{
    public class InvalidShapeDefinitionException : Exception
    {
        public InvalidShapeDefinitionException(Shape Shape, string Comment = "") : base(String.Format("Invalid Shape Definition for Shape: {0} (id: {1})" + Environment.NewLine + "Comment:" + Environment.NewLine + "{2}", Shape.ID, Shape.Code.ToString(), Comment))
        {
        }
    }
}