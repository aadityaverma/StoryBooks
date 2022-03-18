namespace System;

public interface IDateTime
{
    DateOnly Date();

    DateTime Now();

    TimeOnly Time();
}