﻿namespace Battleground.Models.Exceptions;

public class InventoryException : Exception
{
    public InventoryException(string message) : base(message) {}
}