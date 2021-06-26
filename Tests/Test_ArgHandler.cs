using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    static class Tests_ArgHandler {
        public static bool WriteAndReturn(string text, params string[] toFormat) {
            Console.WriteLine(text, toFormat);
            return true;
        }

        private static Dictionary<string, WalkmanLib.FlagInfo> flagDict = new() {
            {"test", new WalkmanLib.FlagInfo() {
                shortFlag = 't',
                hasArgs = true,
                argsInfo = "<string>",
                description = "test",
                action = (arg) => WriteAndReturn("1{0}", arg)
            }},
            {"test2", new WalkmanLib.FlagInfo() {
                hasArgs = false,
                description = "test2",
                action = (arg) => WriteAndReturn("test2 called")
            }},
            {"test3", new WalkmanLib.FlagInfo() {
                hasArgs = true,
                argsInfo = "<string>",
                description = "test3",
                action = (arg) => WriteAndReturn("3{0}", arg)
            }},
            {"test4", new WalkmanLib.FlagInfo() {
                shortFlag = 'k',
                hasArgs = true,
                optionalArgs = true,
                argsInfo = "[string]",
                description = "test4",
                action = (arg) => WriteAndReturn("test4 called: {0}", arg)
            }},
            {"test5", new WalkmanLib.FlagInfo() {
                shortFlag = 'T',
                description = "test5",
                action = (arg) => WriteAndReturn("test5 called")
            }},
            {"TEST", new WalkmanLib.FlagInfo() {
                hasArgs = true,
                argsInfo = "<string>",
                description = "TEST",
                action = (arg) => WriteAndReturn("TEST{0}", arg)
            }},
            {"TeSt7", new WalkmanLib.FlagInfo() {
                hasArgs = false,
                description = "TeSt7",
                action = (arg) => WriteAndReturn("TeSt7 called")
            }},
            {"Test8", new WalkmanLib.FlagInfo() {
                hasArgs = false,
                description = "Test8",
                action = (arg) => {
                    Console.WriteLine("returning false");
                    return false;
                }
            }},
            {"help", new WalkmanLib.FlagInfo() {
                shortFlag = 'h',
                description = "Show Help",
                action = (arg) => {
                    WalkmanLib.EchoHelp(flagDict);
                    return true;
                }
            }}
        };

        public static bool Test_ArgHandler1() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.EchoHelp(flagDict);
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "Option        Long Option       Description" + Environment.NewLine + 
                                    " -t <string>  --test=<string>   test" + Environment.NewLine + 
                                    "              --test2           test2" + Environment.NewLine + 
                                    "              --test3=<string>  test3" + Environment.NewLine + 
                                    " -k [string]  --test4=[string]  test4" + Environment.NewLine + 
                                    " -T           --test5           test5" + Environment.NewLine + 
                                    "              --TEST=<string>   TEST" + Environment.NewLine + 
                                    "              --TeSt7           TeSt7" + Environment.NewLine + 
                                    "              --Test8           Test8" + Environment.NewLine + 
                                    " -h           --help            Show Help" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler1", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler2() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.EchoHelp(flagDict, "t");
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "Option        Long Option      Description" + Environment.NewLine +
                                    " -t <string>  --test=<string>  test" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler2", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler3() {
            var sw = new System.IO.StringWriter();
            
            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.EchoHelp(flagDict, "test7");
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "Long Option  Description" + Environment.NewLine + 
                                    "--TeSt7      TeSt7" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler3", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler4() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"-t", "-k", "-T"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "Short Flag \"t\" requires arguments!" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler4", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler5() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"-t", "TEST", "--test2", "--test3=TEST"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "1TEST" + Environment.NewLine + "test2 called" + Environment.NewLine + "3TEST" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler5", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler6() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] { "-T" }, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "test5 called" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler6", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler7() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"--test7"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "TeSt7 called" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler7", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler8() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"-t", "TEST", "--", "--test3=TEST"}, flagDict, true);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    } else {
                        Console.WriteLine(rtn.extraParams.First());
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "1TEST" + Environment.NewLine + "--test3=TEST" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler8", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler9() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"-k"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "test4 called: " + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler9", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler10() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"-k", "shorttest"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "test4 called: shorttest" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler10", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler11() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"--test4"}, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "test4 called: " + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler11", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler12() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] { "--test4=longtest" }, flagDict);
                    if (rtn.gotError) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "test4 called: longtest" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler12", sw.ToString(), expectedOutput);
        }

        public static bool Test_ArgHandler13() {
            var sw = new System.IO.StringWriter();

            using (new RedirectConsole(sw)) {
                try {
                    WalkmanLib.ResultInfo rtn = WalkmanLib.ProcessArgs(new[] {"--test8", "--test4", "--test5", "-h"}, flagDict);
                    if (rtn.gotError && rtn.errorInfo != null) {
                        Console.WriteLine(rtn.errorInfo);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }

            string expectedOutput = "returning false" + Environment.NewLine;

            return GeneralFunctions.TestString("ArgHandler13", sw.ToString(), expectedOutput);
        }
    }
}
