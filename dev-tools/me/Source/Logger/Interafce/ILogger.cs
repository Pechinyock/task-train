﻿namespace Me;

//Verbosity
//LogChannelsEnum
[Flags]
public enum LogChannelsEnum
{
    None = 0,
    Error = 1 << 0,
    Warn = 1 << 1,
    Info = 1 << 2,
    Trace = 1 << 3,

    All = Error | Warn | Info | Trace,
}

public interface ILogger
{
    void SetVerbosity(LogChannelsEnum value);
    void Trace(string msg);
    void Info(string msg);
    void Warn(string msg);
    void Error(string msg);
}
