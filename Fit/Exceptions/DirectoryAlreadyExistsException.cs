﻿namespace Fit.Exceptions;

public class DirectoryAlreadyExistsException : IOException
{
    public DirectoryAlreadyExistsException()
    {
    }

    public DirectoryAlreadyExistsException(string message)
        : base(message)
    {
    }

    public DirectoryAlreadyExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
