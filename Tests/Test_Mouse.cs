using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.VisualBasic.Interaction;

namespace Tests {
    static class Tests_Mouse {
        public static bool Test_Mouse1() {
            Task.Run(() => {
                Thread.Sleep(400);

                Cursor.Position = new System.Drawing.Point(325, 55);
                WalkmanLib.MouseClick(MouseButton.LeftClick);
            });

            string result = InputBox("Press Esc if this window doesn't disappear", DefaultResponse: "test", XPos: 0, YPos: 0);

            return GeneralFunctions.TestString("Mouse1", result, "test");
        }

        public static bool Test_Mouse2() {
            Task.Run(() => {
                Thread.Sleep(400);

                Cursor.Position = new System.Drawing.Point(100, 10);
                WalkmanLib.MouseClick(MouseButton.RightClick);
                Thread.Sleep(10);
                Cursor.Position = new System.Drawing.Point(150, 140);
                WalkmanLib.MouseClick(MouseButton.LeftClick);
            });

            string result = InputBox("Click OK if this window doesn't get closed", DefaultResponse: "test", XPos: 0, YPos: 0);

            return GeneralFunctions.TestString("Mouse2", result, "");
        }
    }
}
