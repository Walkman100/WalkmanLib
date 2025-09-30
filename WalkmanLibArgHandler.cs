using System;
using System.Collections.Generic;
using System.Linq;

public partial class WalkmanLib {
    public const char NullChar = '\0';

    /// <summary>Create a dictionary of these to use with the ArgHandler methods</summary>
    public class FlagInfo {
        /// <summary>Optional single character to activate this flag</summary>
        public char shortFlag = NullChar;
        /// <summary>Set to <see langword="true"/> if this flag requires arguments</summary>
        public bool hasArgs = false;
        /// <summary>Set to <see langword="true"/> if this flag's arguments are optional</summary>
        public bool optionalArgs = false;
        /// <summary>Set to text to describe the flag's argument, if <see cref="hasArgs"/> is <see langword="true"/>. Used in <see cref="EchoHelp"/></summary>
        public string argsInfo = null;
        /// <summary>Description of the flag used in <see cref="EchoHelp"/></summary>
        public string description;
        /// <summary>
        /// Routine to run when the command is called. String argument will be <see langword="null"/> if <see cref="hasArgs"/> is <see langword="false"/>,
        /// or <see cref="optionalArgs"/> is <see langword="true"/> and no argument is supplied.
        /// <br />This function should return a <see cref="bool"/>, return <see langword="false"/> to stop ProcessArgs args processing.
        /// <see cref="ResultInfo.errorInfo"/> will be set to <see langword="null"/>.
        /// </summary>
        public Func<string, bool> action;
    }

    #region EchoHelp & helpers
    /// <summary>Info on whether the dictionary of items has any short flags, and the max length of those flags</summary>
    private class ShortFlagInfo {
        public bool usesShortFlags = false;
        public int maxLength = 0;
    }

    /// <summary>Gets whether the supplied dictionary of items has any short flags, and the max length of those flags</summary>
    /// <param name="flagDict">Dictionary to operate on</param>
    private static ShortFlagInfo getShortFlagInfo(Dictionary<string, FlagInfo> flagDict) {
        var retVal = new ShortFlagInfo();

        foreach (KeyValuePair<string, FlagInfo> flagInfo in flagDict) {
            if (flagInfo.Value.shortFlag != NullChar) {
                retVal.usesShortFlags = true;

                int flagLength = 1;
                if (flagInfo.Value.hasArgs) {
                    flagLength += flagInfo.Value.argsInfo.Length + 1;
                }

                if (flagLength > retVal.maxLength) {
                    retVal.maxLength = flagLength;
                }
            }
        }

        return retVal;
    }

    /// <summary>Gets the maximum length of the flags in the supplied dictionary of items</summary>
    /// <param name="flagDict">Dictionary to operate on</param>
    private static int getMaxFlagLength(Dictionary<string, FlagInfo> flagDict) {
        int retVal = 0;

        foreach (KeyValuePair<string, FlagInfo> flagInfo in flagDict) {
            int flagLength = flagInfo.Key.Length;
            if (flagInfo.Value.hasArgs) {
                flagLength += flagInfo.Value.argsInfo.Length + 1;
            }

            if (flagLength > retVal) {
                retVal = flagLength;
            }
        }

        return retVal;
    }

    /// <summary>Generates and writes help information about the items in the specified dictionary to the console</summary>
    /// <param name="flagDict">Dictionary to operate on</param>
    /// <param name="flag">Optional flag to show help for. Can be the long or short form</param>
    /// <param name="output">Optional TextWriter to write the output to. Defaults to <see cref="Console.Error"/>.</param>
    public static void EchoHelp(Dictionary<string, FlagInfo> flagDict, string flag = null, System.IO.TextWriter output = null) {
        if (output == null) output = Console.Error;
        if (flag == null) {
            ShortFlagInfo shortFlagInfo = getShortFlagInfo(flagDict);
            int maxFlagLength = getMaxFlagLength(flagDict);

            if (shortFlagInfo.usesShortFlags) {
                output.WriteLine("{0}  {1}  {2}", "Option".PadRight(shortFlagInfo.maxLength + 2), "Long Option".PadRight(maxFlagLength + 2), "Description");
            } else {
                output.WriteLine("{0}  {1}", "Long Option".PadRight(maxFlagLength + 2), "Description");
            }

            foreach (KeyValuePair<string, FlagInfo> flagInfo in flagDict) {
                if (flagInfo.Value.shortFlag != NullChar) {
                    string shortFlag;
                    if (flagInfo.Value.hasArgs) {
                        shortFlag = (flagInfo.Value.shortFlag + " " + flagInfo.Value.argsInfo).PadRight(shortFlagInfo.maxLength);
                    } else {
                        shortFlag = flagInfo.Value.shortFlag.ToString().PadRight(shortFlagInfo.maxLength);
                    }
                    output.Write(" -{0}  ", shortFlag);
                } else if (shortFlagInfo.usesShortFlags) {
                    output.Write(" ".PadRight(shortFlagInfo.maxLength + 2) + "  ");
                }

                string longFlag;
                if (flagInfo.Value.hasArgs) {
                    longFlag = flagInfo.Key + "=" + flagInfo.Value.argsInfo;
                } else {
                    longFlag = flagInfo.Key;
                }
                output.WriteLine("--{0}  {1}", longFlag.PadRight(maxFlagLength), flagInfo.Value.description);
            }
        } else {
            KeyValuePair<string, FlagInfo> flagKV = flagDict.FirstOrDefault(
                (KeyValuePair<string, FlagInfo> x) => {
                    if (flag.Length == 1 && x.Value.shortFlag == flag.ToLowerInvariant()[0]) {
                        return true;
                    }

                    return x.Key.ToLowerInvariant() == flag.ToLowerInvariant();
                });

            if (flagKV.Key != null) {
                FlagInfo flagInfo = flagKV.Value;
                string longFlag = flagKV.Key;
                if (flagInfo.hasArgs) {
                    longFlag += "=" + flagInfo.argsInfo;
                }

                if (flagInfo.shortFlag != NullChar) {
                    int optionPad = 4 + (flagInfo.argsInfo ?? "").Length;
                    output.WriteLine("{0}  {1}  {2}", "Option".PadRight(optionPad), "Long Option".PadRight(longFlag.Length + 2), "Description");

                    output.Write(" -{0} {1}  ", flagInfo.shortFlag, (flagInfo.argsInfo ?? "").PadRight(2));
                } else {
                    output.WriteLine("{0}  {1}", "Long Option".PadRight(longFlag.Length + 2), "Description");
                }

                output.WriteLine("--{0}  {1}", longFlag.PadRight(9), flagInfo.description);
            } else {
                output.WriteLine("Flag \"{0}\" not found!", flag);
            }
        }
    }
    #endregion

    #region ProcessArgs & helpers
    /// <summary>Return info for ProcessArgs</summary>
    public class ResultInfo {
        /// <summary>Will be <see langword="true"/> if there was an error. <see cref="errorInfo"/> contains the error message</summary>
        public bool gotError = false;
        /// <summary>Error message. <see langword="null"/> if no error occurred - check <see cref="gotError"/></summary>
        public string errorInfo = null;
        /// <summary>Parameters after processing arguments has completed. Empty list if there are no parameters or <paramref name="ProcessArgs.paramsAfterFlags"/> is <see langword="false"/></summary>
        public List<string> extraParams = new List<string>();
    }

    /// <summary>Internal helper to simplify creating a <see cref="ResultInfo"/> instance, with <c>errorInfo</c> set and <c>gotError = true</c></summary>
    /// <param name="errorInfo">String to fill the <see cref="ResultInfo.errorInfo"/> field with</param>
    /// <param name="flagName">Optional string to format <paramref name="errorInfo"/> - <c>string.Format</c> will be used if this flag is set</param>
    /// <returns></returns>
    private static ResultInfo GetErrorResult(string errorInfo, string flagName = null) {
        var resultInfo = new ResultInfo() { gotError = true };
        if (flagName == null) {
            resultInfo.errorInfo = errorInfo;
        } else {
            resultInfo.errorInfo = string.Format(errorInfo, flagName);
        }
        return resultInfo;
    }

    private static void DisableGettingArg(ref bool gettingArg, ref FlagInfo gettingArgFor, ref string gettingArgForLong) {
        gettingArg = false;
        gettingArgFor = null;
        gettingArgForLong = null;
    }

    /// <summary>
    /// Processes arguments supplied to a program using a supplied Dictionary of flags. If <paramref name="paramsAfterFlags"/> is <see langword="true"/>,
    /// <see cref="ResultInfo.extraParams"/> will contain a list of arguments after processing is complete. If there is an incorrect parameter in the supplied string array,
    /// <see cref="ResultInfo.gotError"/> will be <see langword="true"/> and <see cref="ResultInfo.errorInfo"/> will contain the error message to display to the user.
    /// <br/>Short flags are processed first, and once a long flag is encountered short flag processing is skipped.
    /// <br/>"<c>--</c>" can be used to force flag processing to end, and the remainder of the arguments will be considered as extra parameters.
    /// <br/>Long flag matching is case-insensitive, while Short flag matching is case-sensitive - characters must match exactly.
    /// </summary>
    /// <param name="args">Array of argument strings to process</param>
    /// <param name="flagDict">Dictionary to operate on</param>
    /// <param name="paramsAfterFlags"><see langword="true"/> to allow extra parameters after arguments processing is complete</param>
    /// <returns><see cref="ResultInfo"/> class with the result of processing</returns>
    public static ResultInfo ProcessArgs(string[] args, Dictionary<string, FlagInfo> flagDict, bool paramsAfterFlags = false) {
        bool processingShortFlags = true;
        bool finishedProcessingArgs = false;
        bool gettingArg = false;
        var gettingArgFor = new FlagInfo();
        string gettingArgForLong = null;
        var extraParams = new List<string>();

        foreach (string preArg in args) {
            string arg = preArg; // apparently you can't set foreach variables in C# like you can in VB.Net

            if (processingShortFlags && arg.StartsWith("-") && arg.Length > 1 && arg[1] != '-') {
                foreach (char chr in arg.Substring(1)) {
                    if (gettingArg && gettingArgFor.optionalArgs) {
                        if (!gettingArgFor.action(null)) return GetErrorResult(null);
                        DisableGettingArg(ref gettingArg, ref gettingArgFor, ref gettingArgForLong);
                    } else if (gettingArg) {
                        return GetErrorResult("Short Flag \"{0}\" requires arguments!", gettingArgFor.shortFlag.ToString());
                    }

                    KeyValuePair<string, FlagInfo> flagKV = flagDict.FirstOrDefault(x => x.Value.shortFlag == chr);

                    if (flagKV.Value != null) {
                        if (flagKV.Value.hasArgs) {
                            gettingArg = true;
                            gettingArgFor = flagKV.Value;
                        } else {
                            if (!flagKV.Value.action(null)) return GetErrorResult(null);
                        }
                    } else {
                        return GetErrorResult("Unknown Short Flag \"{0}\"!", chr.ToString());
                    }
                }
            } else if (!arg.StartsWith("--") && gettingArg) {
                if (!gettingArgFor.action(arg)) return GetErrorResult(null);

                DisableGettingArg(ref gettingArg, ref gettingArgFor, ref gettingArgForLong);
            } else if (arg == "--") {
                if (gettingArg && gettingArgFor.optionalArgs) {
                    if (!gettingArgFor.action(null)) return GetErrorResult(null);
                    DisableGettingArg(ref gettingArg, ref gettingArgFor, ref gettingArgForLong);
                } else if (gettingArg) {
                    return GetErrorResult("Flag \"{0}\" requires arguments!", gettingArgForLong ?? gettingArgFor.shortFlag.ToString());
                }

                processingShortFlags = false;
                finishedProcessingArgs = true;
            } else if (!finishedProcessingArgs && arg.StartsWith("--")) {
                if (gettingArg && gettingArgFor.optionalArgs) {
                    if (!gettingArgFor.action(null)) return GetErrorResult(null);
                    DisableGettingArg(ref gettingArg, ref gettingArgFor, ref gettingArgForLong);
                } else if (gettingArg) {
                    return GetErrorResult("Flag \"{0}\" requires arguments!", gettingArgForLong ?? gettingArgFor.shortFlag.ToString());
                }

                processingShortFlags = false;

                arg = arg.Substring(2);
                string flagArg = null;
                if (arg.Contains("=")) {
                    flagArg = arg.Substring(arg.LastIndexOf('=') + 1);
                    arg = arg.Remove(arg.IndexOf('='));
                }

                KeyValuePair<string, FlagInfo> flagKV = flagDict.FirstOrDefault((KeyValuePair<string, FlagInfo> x) =>
                        x.Key.ToLowerInvariant() == arg.ToLowerInvariant());
                if (flagKV.Key != null) {
                    gettingArgFor = flagKV.Value;
                    if (gettingArgFor.hasArgs == false) {
                        if (!gettingArgFor.action(null)) return GetErrorResult(null);
                    } else if (flagArg == null && gettingArgFor.optionalArgs) {
                        if (!gettingArgFor.action(null)) return GetErrorResult(null);
                    } else if (flagArg == null) {
                        return GetErrorResult("Flag \"{0}\" requires arguments!", flagKV.Key);
                    } else {
                        if (!gettingArgFor.action(flagArg)) return GetErrorResult(null);
                    }
                    gettingArgFor = null;
                } else {
                    return GetErrorResult("Unknown Flag \"{0}\"!", arg);
                }
            } else if (paramsAfterFlags == true) {
                extraParams.Add(arg);
            } else {
                return GetErrorResult("Argument \"{0}\" is not a flag!", arg);
            }
        }

        if (gettingArg && gettingArgFor.optionalArgs) {
            if (!gettingArgFor.action(null)) return GetErrorResult(null);
            DisableGettingArg(ref gettingArg, ref gettingArgFor, ref gettingArgForLong);
        } else if (gettingArg) {
            return GetErrorResult("Flag \"{0}\" requires arguments!", gettingArgForLong ?? gettingArgFor.shortFlag.ToString());
        }

        return new ResultInfo() {
            gotError = false,
            extraParams = extraParams
        };
    }
    #endregion
}
